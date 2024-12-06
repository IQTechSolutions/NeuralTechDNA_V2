using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NeuralTech.Entities;
using NeuralTech.EntityFramework.Interfaces;
using NeuralTech.Interfaces;
using NeuralTech.ResultWrappers;

namespace NeuralTech.EntityFramework.Context
{
    /// <summary>
    /// Represents a generic repository for performing CRUD operations on entities.
    /// Provides methods for creating, reading, updating, and deleting entities,
    /// as well as querying entities with optional eager loading and change tracking.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the entity's key.</typeparam>
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        /// <summary>
        /// The database context used for data access operations.
        /// </summary>
        private readonly AuditableContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity, TKey}"/> class.
        /// Ensures that the provided context is not null.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <exception cref="ArgumentNullException">Thrown when the context is null.</exception>
        public Repository(AuditableContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Handles exceptions and returns a failed result with the base exception message.
        /// This method centralizes exception handling for consistency.
        /// </summary>
        /// <typeparam name="T">The type of the result data.</typeparam>
        /// <param name="ex">The exception to handle.</param>
        /// <returns>A failed result containing the base exception message.</returns>
        private static IBaseResult<T> HandleException<T>(Exception ex)
        {
            // Get the base exception to find the root cause of the error
            var baseException = ex.GetBaseException();

            // Return a failed result containing the exception message
            return Result<T>.Fail(baseException.Message);
        }

        /// <summary>
        /// Applies include expressions to the query for eager loading related entities.
        /// Uses expression trees to specify the related entities to include.
        /// </summary>
        /// <param name="query">The query to which includes will be applied.</param>
        /// <param name="includes">The include expressions specifying related entities.</param>
        /// <returns>The query with includes applied for eager loading.</returns>
        private IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        {
            // Aggregate the includes, applying each one to the query
            return includes.Aggregate(query, (current, include) => current.Include(include));
        }

        /// <summary>
        /// Asynchronously retrieves all entities from the database.
        /// Allows optional tracking of changes and eager loading of related entities.
        /// </summary>
        /// <param name="trackChanges">If true, the entities will be tracked for changes; otherwise, no tracking is applied.</param>
        /// <param name="includes">Optional expressions specifying related entities to include.</param>
        /// <returns>A task containing a result with a list of all entities.</returns>
        public async Task<IBaseResult<List<TEntity>>> FindAllAsync(bool trackChanges, params Expression<Func<TEntity, object>>[] includes)
        {
            try
            {
                // Start with the DbSet for the entity type
                var query = trackChanges
                    ? _context.Set<TEntity>() // Tracking changes enables entity state management
                    : _context.Set<TEntity>().AsNoTracking(); // No tracking improves performance when changes are not needed

                // Apply includes for eager loading of related entities
                query = ApplyIncludes(query, includes);

                // Execute the query and retrieve the data as a list
                var entities = await query.ToListAsync();

                // Return a successful result containing the list of entities
                return Result<List<TEntity>>.Success(entities);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a failed result
                return HandleException<List<TEntity>>(ex);
            }
        }

        /// <summary>
        /// Asynchronously finds entities that match a specified condition.
        /// Allows optional tracking of changes and eager loading of related entities.
        /// </summary>
        /// <param name="expression">A lambda expression representing the condition to filter entities.</param>
        /// <param name="trackChanges">If true, the entities will be tracked for changes; otherwise, no tracking is applied.</param>
        /// <param name="includes">Optional expressions specifying related entities to include.</param>
        /// <returns>A task containing a result with a list of entities matching the condition.</returns>
        public async Task<IBaseResult<List<TEntity>>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression, bool trackChanges, params Expression<Func<TEntity, object>>[] includes)
        {
            try
            {
                // Apply the filter condition to the DbSet
                var query = trackChanges
                    ? _context.Set<TEntity>().Where(expression)
                    : _context.Set<TEntity>().Where(expression).AsNoTracking();

                // Apply includes for eager loading of related entities
                query = ApplyIncludes(query, includes);

                // Execute the query and retrieve the data as a list
                var entities = await query.ToListAsync();

                // Return a successful result containing the list of entities
                return Result<List<TEntity>>.Success(entities);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a failed result
                return HandleException<List<TEntity>>(ex);
            }
        }

        /// <summary>
        /// Asynchronously creates a new entity in the context.
        /// The changes are not saved to the database until <see cref="SaveAsync"/> is called.
        /// </summary>
        /// <param name="entity">The entity to add to the context.</param>
        /// <returns>A task containing a result with the added entity.</returns>
        public async Task<IBaseResult<TEntity>> CreateAsync(TEntity entity)
        {
            try
            {
                // Add the entity to the context; changes are tracked but not yet saved
                await _context.Set<TEntity>().AddAsync(entity);

                // Return a successful result containing the entity
                return Result<TEntity>.Success(entity);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a failed result
                return HandleException<TEntity>(ex);
            }
        }

        /// <summary>
        /// Asynchronously updates an existing entity in the context.
        /// The changes are not saved to the database until <see cref="SaveAsync"/> is called.
        /// </summary>
        /// <param name="entity">The entity with updated values.</param>
        /// <returns>A task containing a result with the updated entity.</returns>
        public async Task<IBaseResult<TEntity>> UpdateAsync(TEntity entity)
        {
            try
            {
                // Update the entity in the context; changes are tracked but not yet saved
                _context.Set<TEntity>().Update(entity);

                // Return a successful result containing the entity
                return Result<TEntity>.Success(entity);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a failed result
                return HandleException<TEntity>(ex);
            }
        }

        /// <summary>
        /// Asynchronously deletes an entity from the context.
        /// The changes are not saved to the database until <see cref="SaveAsync"/> is called.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        /// <returns>A task containing a result indicating the success of the operation.</returns>
        public async Task<IBaseResult<TEntity>> DeleteAsync(TEntity entity)
        {
            try
            {
                // Remove the entity from the context; changes are tracked but not yet saved
                _context.Set<TEntity>().Remove(entity);

                // Return a successful result
                return Result<TEntity>.Success(entity);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a failed result
                return HandleException<TEntity>(ex);
            }
        }

        /// <summary>
        /// Asynchronously deletes an entity identified by its key from the context.
        /// The changes are not saved to the database until <see cref="SaveAsync"/> is called.
        /// </summary>
        /// <param name="id">The key of the entity to remove.</param>
        /// <returns>A task containing a result indicating the success of the operation.</returns>
        public async Task<IBaseResult<TEntity>> DeleteAsync(TKey id)
        {
            try
            {
                // Find the entity by its key
                var entity = await _context.Set<TEntity>().FindAsync(id);

                // If the entity is not found, return a failed result
                if (entity == null)
                    return Result<TEntity>.Fail($"Entity with ID {id} not found.");

                // Remove the entity from the context; changes are tracked but not yet saved
                _context.Set<TEntity>().Remove(entity);

                // Return a successful result
                return Result<TEntity>.Success(entity);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a failed result
                return HandleException<TEntity>(ex);
            }
        }

        /// <summary>
        /// Asynchronously saves all pending changes in the context to the database.
        /// This method should be called after creating, updating, or deleting entities.
        /// </summary>
        /// <returns>A task containing a result indicating the success of the save operation.</returns>
        public async Task<IBaseResult> SaveAsync()
        {
            try
            {
                // Save all changes made in the context to the database
                await _context.SaveChangesAsync();

                // Return a successful result indicating that changes were saved
                return Result.Success();
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a failed result
                return HandleException<object>(ex);
            }
        }
    }
}
