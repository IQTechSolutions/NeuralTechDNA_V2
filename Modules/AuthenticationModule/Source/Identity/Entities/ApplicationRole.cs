using Microsoft.AspNetCore.Identity;
using NeuralTech.Interfaces;

namespace Identity.Entities
{
    /// <summary>
    /// Represents an application role with additional auditing properties.
    /// </summary>
    public class ApplicationRole : IdentityRole, IAuditableEntity<string>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRole"/> class.
        /// </summary>
        public ApplicationRole()
        {
            CreatedBy = string.Empty;
            RoleClaims = new HashSet<ApplicationRoleClaim>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRole"/> class with a specified role name and description.
        /// </summary>
        /// <param name="roleName">The name of the role.</param>
        /// <param name="roleDescription">The description of the role.</param>
        public ApplicationRole(string roleName, string roleDescription = null) : base(roleName)
        {
            Description = roleDescription;
            CreatedBy = string.Empty;
            RoleClaims = new HashSet<ApplicationRoleClaim>();
        }

        #endregion

        /// <summary>
        /// Gets or sets the description of the role.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the user who created the role.
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the role was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the user who last modified the role.
        /// </summary>
        public string? LastModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the role was last modified.
        /// </summary>
        public DateTime? LastModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets the collection of role claims associated with this role.
        /// </summary>
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return "Application Role";
        }
    }
}