namespace NeuralTech.Interfaces
{
    /// <summary>
    /// Interface for auditable entities, providing properties for tracking creation and modification details.
    /// </summary>
    public interface IAuditableEntity 
    {
        /// <summary>
        /// Gets or sets the user who created the entity.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the user who last modified the entity.
        /// </summary>
        public string? LastModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last modified.
        /// </summary>
        public DateTime? LastModifiedOn { get; set; }
    }

    /// <summary>
    /// Interface for auditable entities with a specific identifier type, inheriting from IAuditableEntity and IEntity.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    public interface IAuditableEntity<TId> : IAuditableEntity
    {
        public TId Id { get; set; }
    }
}
