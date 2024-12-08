using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuralTech.Entities;
using NeuralTech.Interfaces;

namespace Grouping.Entities
{
    /// <summary>
    /// Represents the association between an entity and a category.
    /// </summary>
    /// <typeparam name="T">The type of the auditable entity.</typeparam>
    public class EntityCategory<T> : EntityBase<string> where T : IAuditableEntity<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCategory{T}"/> class with specified entity and category IDs.
        /// </summary>
        /// <param name="entityId">The ID of the entity.</param>
        /// <param name="categoryId">The ID of the category.</param>
        public EntityCategory(string entityId, string categoryId)
        {
            EntityId = entityId;
            CategoryId = categoryId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCategory{T}"/> class with specified entity and category objects.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        /// <param name="category">The category object.</param>
        public EntityCategory(T entity, Category<T> category)
        {
            EntityId = entity.Id;
            Entity = entity;
            CategoryId = category.Id;
            Category = category;
        }

        /// <summary>
        /// Gets or sets the ID of the entity.
        /// </summary>
        [ForeignKey(nameof(Entity))]
        [Required(ErrorMessage = "Entity ID is required.")]
        public string EntityId { get; set; }

        /// <summary>
        /// Gets or sets the entity object.
        /// </summary>
        public T Entity { get; set; }

        /// <summary>
        /// Gets or sets the ID of the category.
        /// </summary>
        [ForeignKey(nameof(Category))]
        [Required(ErrorMessage = "Category ID is required.")]
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category object.
        /// </summary>
        public Category<T> Category { get; set; }

        /// <summary>
        /// Returns a string representation of the entity-category association.
        /// </summary>
        /// <returns>A string representation of the entity-category association.</returns>
        public override string ToString()
        {
            return $"Category for {typeof(T).Name}";
        }
    }
}

