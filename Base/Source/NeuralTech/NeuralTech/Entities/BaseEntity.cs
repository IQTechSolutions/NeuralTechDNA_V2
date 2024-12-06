using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using NeuralTech.Interfaces;

namespace NeuralTech.Entities
{
    /// <summary>
    /// Abstract base class for entities, providing common properties for auditing and identity.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    public abstract class EntityBase<TId> : IAuditableEntity<TId>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, DisplayName("Id"), Column("Id")]
        public TId Id { get; set; }

        /// <summary>
        /// Gets or sets the user who created the entity.
        /// </summary>
        public string CreatedBy { get; set; } = "";

        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the user who last modified the entity.
        /// </summary>
        public string? LastModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last modified.
        /// </summary>
        public DateTime? LastModifiedOn { get; set; }
    }
}
