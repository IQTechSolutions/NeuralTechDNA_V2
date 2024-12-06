using NeuralTech.Interfaces;
using Filing.Entities;

namespace Filing.Interfaces
{
    /// <summary>
    /// Represents a collection of image files associated with an entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    public interface IImageFileCollection<TEntity, TId> : IAuditableEntity<TId> where TEntity : IAuditableEntity<TId>
    {
        /// <summary>
        /// Gets or sets the collection of image files associated with the entity.
        /// </summary>
        ICollection<ImageFile<TEntity, TId>> Images { get; set; }
    }
}