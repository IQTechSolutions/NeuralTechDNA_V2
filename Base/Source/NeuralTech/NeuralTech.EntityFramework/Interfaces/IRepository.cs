using System.Linq.Expressions;
using NeuralTech.Entities;
using NeuralTech.Interfaces;

namespace NeuralTech.EntityFramework.Interfaces
{
    /// <summary>
    /// Defines a generic repository interface for performing CRUD operations on entities.
    /// Provides methods for creating, reading, updating, and deleting entities,
    /// as well as querying entities with optional eager loading and change tracking.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the entity's key.</typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : IAuditableEntity<TKey>
    {
        /// <summary>
        /// Asynchronously retrieves all entities from the database.
        /// Allows optional tracking of changes and eager loading of related entities.
        /// </summary>
        /// <param name="trackChanges">
        /// If <c>true</c>, the entities will be tracked for changes; otherwise, no tracking is applied.
        /// Tracking allows updates to be detected when <see cref="SaveAsync"/> is called.
        /// </param>
        /// <param name="includes">
        /// Optional expressions specifying related entities to include for eager loading.
        /// Example: <c>entity => entity.RelatedEntities</c>.
        /// </param>
        /// <returns>
        /// A task containing a result with a list of all entities.
        /// The result indicates success or failure and contains any relevant data or error messages.
        /// </returns>
        Task<IBaseResult<List<TEntity>>> FindAllAsync(bool trackChanges, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Asynchronously finds entities that match a specified condition.
        /// Allows optional tracking of changes and eager loading of related entities.
        /// </summary>
        /// <param name="expression">
        /// A lambda expression representing the condition to filter entities.
        /// Example: <c>entity => entity.Property == value</c>.
        /// </param>
        /// <param name="trackChanges">
        /// If <c>true</c>, the entities will be tracked for changes; otherwise, no tracking is applied.
        /// </param>
        /// <param name="includes">
        /// Optional expressions specifying related entities to include for eager loading.
        /// </param>
        /// <returns>
        /// A task containing a result with a list of entities matching the condition.
        /// The result indicates success or failure and contains any relevant data or error messages.
        /// </returns>
        Task<IBaseResult<List<TEntity>>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression, bool trackChanges, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Asynchronously creates a new entity in the context.
        /// The changes are not saved to the database until <see cref="SaveAsync"/> is called.
        /// </summary>
        /// <param name="entity">
        /// The entity to add to the context.
        /// Must not be <c>null</c>.
        /// </param>
        /// <returns>
        /// A task containing a result with the added entity.
        /// The result indicates success or failure and contains any relevant data or error messages.
        /// </returns>
        Task<IBaseResult<TEntity>> CreateAsync(TEntity entity);

        /// <summary>
        /// Asynchronously updates an existing entity in the context.
        /// The changes are not saved to the database until <see cref="SaveAsync"/> is called.
        /// </summary>
        /// <param name="entity">
        /// The entity with updated values.
        /// The entity must be tracked by the context.
        /// </param>
        /// <returns>
        /// A task containing a result with the updated entity.
        /// The result indicates success or failure and contains any relevant data or error messages.
        /// </returns>
        Task<IBaseResult<TEntity>> UpdateAsync(TEntity entity);

        /// <summary>
        /// Asynchronously deletes an entity from the context.
        /// The changes are not saved to the database until <see cref="SaveAsync"/> is called.
        /// </summary>
        /// <param name="entity">
        /// The entity to remove.
        /// The entity must be tracked by the context.
        /// </param>
        /// <returns>
        /// A task containing a result indicating the success of the operation.
        /// The result includes the deleted entity if successful.
        /// </returns>
        Task<IBaseResult<TEntity>> DeleteAsync(TEntity entity);

        /// <summary>
        /// Asynchronously deletes an entity identified by its key from the context.
        /// The changes are not saved to the database until <see cref="SaveAsync"/> is called.
        /// </summary>
        /// <param name="id">
        /// The key of the entity to remove.
        /// Must match the type <typeparamref name="TKey"/>.
        /// </param>
        /// <returns>
        /// A task containing a result indicating the success of the operation.
        /// The result includes the deleted entity if successful.
        /// If the entity is not found, the result indicates failure.
        /// </returns>
        Task<IBaseResult<TEntity>> DeleteAsync(TKey id);

        /// <summary>
        /// Asynchronously saves all pending changes in the context to the database.
        /// This method should be called after creating, updating, or deleting entities.
        /// </summary>
        /// <returns>
        /// A task containing a result indicating the success of the save operation.
        /// The result includes error information if the save fails.
        /// </returns>
        Task<IBaseResult> SaveAsync();
    }
}
