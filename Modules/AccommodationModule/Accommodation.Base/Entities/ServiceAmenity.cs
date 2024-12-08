using System.ComponentModel.DataAnnotations;
using NeuralTech.Entities;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents a service amenity available in a room within the Accommodation system.
    /// </summary>
    public class ServiceAmenity : EntityBase<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceAmenity"/> class.
        /// </summary>
        public ServiceAmenity() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceAmenity"/> class with a specified name.
        /// </summary>
        /// <param name="name">The name of the service amenity.</param>
        public ServiceAmenity(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets or sets the name of the service amenity.
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(500, ErrorMessage = "Name cannot exceed 500 characters.")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ID of the room associated with this service amenity.
        /// </summary>
        [Required(ErrorMessage = "Room ID is required.")]
        public int RoomId { get; set; }

        /// <summary>
        /// Returns a string representation of the service amenity.
        /// </summary>
        /// <returns>A string that represents the current service amenity.</returns>
        public override string ToString()
        {
            return $"Service Amenity: {Name}";
        }
    }
}