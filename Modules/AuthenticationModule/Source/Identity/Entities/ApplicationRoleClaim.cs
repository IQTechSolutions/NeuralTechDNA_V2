using Microsoft.AspNetCore.Identity;
using NeuralTech.Interfaces;

namespace Identity.Entities
{
    /// <summary>
    /// Represents a claim associated with an application role, including auditing properties.
    /// </summary>
    public class ApplicationRoleClaim : IdentityRoleClaim<string>, IAuditableEntity<int>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRoleClaim"/> class.
        /// </summary>
        public ApplicationRoleClaim() : base()
        {
            CreatedBy = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRoleClaim"/> class with a specified description and group.
        /// </summary>
        /// <param name="roleClaimDescription">The description of the role claim.</param>
        /// <param name="roleClaimGroup">The group of the role claim.</param>
        public ApplicationRoleClaim(string? roleClaimDescription = null, string? roleClaimGroup = null) : base()
        {
            Description = roleClaimDescription;
            Group = roleClaimGroup;
            CreatedBy = string.Empty;
        }

        #endregion

        /// <summary>
        /// Gets or sets the description of the role claim.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the group of the role claim.
        /// </summary>
        public string? Group { get; set; }

        /// <summary>
        /// Gets or sets the user who created the role claim.
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the role claim was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the user who last modified the role claim.
        /// </summary>
        public string? LastModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the role claim was last modified.
        /// </summary>
        public DateTime? LastModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets the associated application role.
        /// </summary>
        public virtual ApplicationRole Role { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return "Application Role Claim";
        }
    }
}

