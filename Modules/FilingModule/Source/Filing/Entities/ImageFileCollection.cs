using Filing.Interfaces;
using NeuralTech.Entities;
using NeuralTech.Interfaces;

namespace Filing.Entities
{
    /// <summary>
    /// Represents a collection of image files associated with an entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    public class ImageFileCollection<TEntity, TId> : EntityBase<TId>, IImageFileCollection<TEntity, TId> where TEntity : IAuditableEntity<TId>
    {
        /// <summary>
        /// Gets or sets the collection of image files associated with the entity.
        /// </summary>
        public virtual ICollection<ImageFile<TEntity, TId>> Images { get; set; } = new List<ImageFile<TEntity, TId>>();
    }
}
