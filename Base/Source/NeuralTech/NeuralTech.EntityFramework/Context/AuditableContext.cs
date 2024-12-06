using System.Security.Claims;
using Identity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NeuralTech.EntityFramework.Entities;
using NeuralTech.Enums;
using NeuralTech.Interfaces;

namespace NeuralTech.EntityFramework.Context;

/// <summary>
/// Represents an auditable DbContext that integrates with ASP.NET Core Identity and tracks changes for auditing purposes.
/// </summary>
/// <remarks>
/// This context overrides the SaveChangesAsync methods to capture audit information for create, update, and delete operations.
/// It uses the IHttpContextAccessor to retrieve the current user performing the changes.
/// </remarks>
public abstract class AuditableContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, ApplicationRoleClaim, IdentityUserToken<string>>
{
    /// <summary>
    /// Provides access to the current HTTP context.
    /// </summary>
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuditableContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by a DbContext.</param>
    /// <param name="httpContextAccessor">The HTTP context accessor.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpContextAccessor"/> is null.</exception>
    protected AuditableContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    /// <summary>
    /// Gets or sets the DbSet for audit trails.
    /// </summary>
    public DbSet<Audit> AuditTrails { get; set; }

    /// <summary>
    /// Asynchronously saves all changes made in this context to the database with a specified user ID.
    /// This method is called internally to perform the actual save operation after preparing audit entries.
    /// </summary>
    /// <param name="userId">The ID of the user performing the changes.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.
    /// </returns>
    public virtual async Task<int> SaveChangesAsync(string userId, CancellationToken cancellationToken = new())
    {
        var auditEntries = OnBeforeSaveChanges(userId);
        var result = await base.SaveChangesAsync(cancellationToken);
        await OnAfterSaveChanges(auditEntries, cancellationToken);
        return result;
    }

    /// <summary>
    /// Processes entities before saving changes to generate audit entries.
    /// </summary>
    /// <param name="userId">The ID of the user performing the changes.</param>
    /// <returns>
    /// A list of audit entries that have temporary properties, which will be processed after saving changes.
    /// </returns>
    /// <remarks>
    /// This method creates audit entries for entities that are being added, modified, or deleted.
    /// It adds audit entries to the <see cref="AuditTrails"/> DbSet for entities that do not have temporary properties.
    /// For entities with temporary properties (e.g., auto-generated primary keys), it defers adding the audit entries until after the changes are saved.
    /// </remarks>
    private List<AuditEntry> OnBeforeSaveChanges(string userId)
    {
        var auditEntries = GetAuditEntries(userId);

        foreach (var auditEntry in auditEntries.Where(ae => !ae.HasTemporaryProperties))
        {
            AuditTrails.Add(auditEntry.ToAudit(userId));
        }
        return auditEntries.Where(ae => ae.HasTemporaryProperties).ToList();
    }

    /// <summary>
    /// Asynchronously saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token to observe while waiting for the task to complete.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.
    /// </returns>
    /// <remarks>
    /// This method overrides the default SaveChangesAsync to include auditing functionality.
    /// It retrieves the current user ID from the HTTP context and updates audit information accordingly.
    /// </remarks>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userId))
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        else
        {
            // Update audit fields for auditable entities
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        entry.Entity.CreatedBy = userId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = userId;
                        break;
                }
            }

            return await SaveChangesAsync(userId, cancellationToken);
        }
    }

    /// <summary>
    /// Retrieves the list of audit entries for entities that are being tracked by the context.
    /// </summary>
    /// <param name="userId">The ID of the user performing the changes.</param>
    /// <returns>
    /// A list of <see cref="AuditEntry"/> representing the changes to be audited.
    /// </returns>
    /// <remarks>
    /// This method inspects the ChangeTracker entries and creates audit entries for entities that are being added, modified, or deleted.
    /// It collects information such as the old and new values of properties, the type of operation, and the affected columns.
    /// </remarks>
    private List<AuditEntry> GetAuditEntries(string userId)
    {
        ChangeTracker.DetectChanges();
        var auditEntries = new List<AuditEntry>();
        foreach (var entry in ChangeTracker.Entries())
        {
            // Skip entities that are not relevant for auditing
            if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                continue;

            var auditEntry = new AuditEntry(entry)
            {
                TableName = entry.Entity.GetType().Name,
                UserId = userId,
                ActionType = GetAuditActionType(entry.State)
            };
            auditEntries.Add(auditEntry);

            // Iterate over the properties to capture changes
            foreach (var property in entry.Properties)
            {
                if (property.IsTemporary)
                {
                    // Property value will be generated by the database (e.g., identity column), so defer processing
                    auditEntry.TemporaryProperties.Add(property);
                    continue;
                }

                string propertyName = property.Metadata.Name;
                if (property.Metadata.IsPrimaryKey())
                {
                    // Capture primary key values
                    auditEntry.KeyValues[propertyName] = property.CurrentValue;
                    continue;
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntry.ActionType = AuditActionType.Create;
                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                        auditEntry.ChangedColumns.Add(propertyName);
                        break;

                    case EntityState.Deleted:
                        auditEntry.ActionType = AuditActionType.Delete;
                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                        auditEntry.ChangedColumns.Add(propertyName);
                        break;

                    case EntityState.Modified:
                        if (property.IsModified && !Equals(property.OriginalValue, property.CurrentValue))
                        {
                            auditEntry.ChangedColumns.Add(propertyName);
                            auditEntry.ActionType = AuditActionType.Update;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                        }
                        break;
                }
            }
        }
        return auditEntries;
    }

    /// <summary>
    /// Processes entities after saving changes to update audit entries with any temporary property values.
    /// </summary>
    /// <param name="auditEntries">A list of audit entries that have temporary properties.</param>
    /// <param name="cancellationToken">
    /// A cancellation token to observe while waiting for the task to complete.
    /// </param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// After the changes have been saved to the database, this method updates the audit entries with any database-generated values (e.g., identity columns).
    /// It then saves the updated audit entries to the database.
    /// </remarks>
    private async Task OnAfterSaveChanges(List<AuditEntry> auditEntries, CancellationToken cancellationToken = new())
    {
        if (auditEntries == null || auditEntries.Count == 0) return;

        var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        foreach (var auditEntry in auditEntries)
        {
            // Update temporary properties with the actual values generated by the database
            foreach (var prop in auditEntry.TemporaryProperties)
            {
                if (prop.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                }
                else
                {
                    auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                }
            }
            // Add the updated audit entry to the AuditTrails DbSet
            AuditTrails.Add(auditEntry.ToAudit(userId));
        }

        // Save the updated audit entries
        await base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Maps the Entity Framework <see cref="EntityState"/> to the corresponding <see cref="AuditActionType"/>.
    /// </summary>
    /// <param name="state">The <see cref="EntityState"/> representing the state of the entity.</param>
    /// <returns>
    /// An <see cref="AuditActionType"/> indicating the type of audit action 
    /// (Create, Update, Delete, or None) based on the entity state.
    /// </returns>
    private AuditActionType GetAuditActionType(EntityState state)
    {
        return state switch
        {
            EntityState.Added => AuditActionType.Create,
            EntityState.Modified => AuditActionType.Update,
            EntityState.Deleted => AuditActionType.Delete,
            _ => AuditActionType.None
        };
    }
}