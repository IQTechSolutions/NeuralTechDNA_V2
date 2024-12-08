using NeuralTech.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents the relationship between an amenity and a given lodging or room.
    /// This entity associates a specific amenity with either a lodging (e.g., a hotel) or a room (e.g., a hotel room).
    /// It can be used to determine which amenities are available at a particular lodging or within a particular room.
    /// </summary>
    /// <typeparam name="TEntity">The entity type to which the amenity is associated (e.g., Lodging or Room).</typeparam>
    /// <typeparam name="TId">The type of the unique identifier for this entity (e.g., int, Guid).</typeparam>
    public class AmenityItem<TEntity, TId> : EntityBase<TId>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AmenityItem{TEntity, TId}"/> class.
        /// Use this parameterless constructor when the entity is created via reflection or dependency injection.
        /// </summary>
        public AmenityItem() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AmenityItem{TEntity, TId}"/> class 
        /// by associating an amenity to a lodging.
        /// </summary>
        /// <param name="lodgingId">The unique identifier of the lodging to which the amenity applies.</param>
        /// <param name="amenityId">The unique identifier of the amenity.</param>
        public AmenityItem(string lodgingId, int amenityId)
        {
            LodgingId = lodgingId;
            AmenityId = amenityId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AmenityItem{TEntity, TId}"/> class 
        /// by associating an amenity to a room within a lodging.
        /// </summary>
        /// <param name="roomId">The unique identifier of the room to which the amenity applies.</param>
        /// <param name="amenityId">The unique identifier of the amenity.</param>
        public AmenityItem(int roomId, int amenityId)
        {
            RoomId = roomId;
            AmenityId = amenityId;
        }

        #endregion

        /// <summary>
        /// Gets or sets the unique identifier of the associated amenity.
        /// This field creates a foreign key relationship to the <see cref="Amenity"/> entity.
        /// </summary>
        [ForeignKey(nameof(Amenity))]
        public int AmenityId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Amenity"/> entity associated with this item.
        /// This navigation property provides access to details about the specific amenity.
        /// </summary>
        public Amenity Amenity { get; set; } = null!;

        /// <summary>
        /// Gets or sets the unique identifier of the lodging that has this amenity.
        /// If this value is set, it indicates that the amenity applies at the lodging level.
        /// </summary>
        [ForeignKey(nameof(Lodging))]
        public string? LodgingId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Lodging"/> entity associated with this amenity item.
        /// When not null, it means the amenity is associated with the entire lodging (e.g., a hotel).
        /// </summary>
        public Lodging? Lodging { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the room that has this amenity.
        /// If this value is set, it indicates that the amenity applies at the room level.
        /// </summary>
        [ForeignKey(nameof(Room))]
        public int? RoomId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Room"/> entity associated with this amenity item.
        /// When not null, it means the amenity is specifically associated with a particular room.
        /// </summary>
        public Room? Room { get; set; }

        /// <summary>
        /// Returns a string representation of the amenity item, indicating to which entity type it belongs.
        /// Useful for debugging and logging.
        /// </summary>
        /// <returns>A string that identifies the amenity item and its associated entity type.</returns>
        public override string ToString()
        {
            return $"Amenity Item for {typeof(TEntity).Name}";
        }
    }
}
