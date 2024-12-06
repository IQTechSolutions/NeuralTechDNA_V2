using System.ComponentModel.DataAnnotations.Schema;
using NeuralTech.Interfaces;

namespace Filing.Entities
{
    /// <summary>
    /// Represents a profile link entity that contains a collection of image files and is associated with another entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the associated entity.</typeparam>
    public class ProfileLink<TEntity> : ImageFileCollection<ProfileLink<TEntity>, string> where TEntity : IAuditableEntity<string>
    {
        /// <summary>
        /// Gets or sets the name of the profile link.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the URL of the profile link.
        /// </summary>
        public string Url { get; set; } = null!;

        #region Relationships

        /// <summary>
        /// Gets or sets the identifier of the associated entity.
        /// </summary>
        [ForeignKey(nameof(Entity))]
        public string? EntityId { get; set; }

        /// <summary>
        /// Gets or sets the associated entity.
        /// </summary>
        public TEntity? Entity { get; set; }

        #endregion

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Profile Link";
        }
    }
}