using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuralTech.Entities;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents a type of bed available in a room within the Accommodation system.
    /// </summary>
    public class BedType : EntityBase<string>
    {
        /// <summary>
        /// Gets or sets the external identifier for the bed type provided by a partner system.
        /// </summary>
        [StringLength(100, ErrorMessage = "Partner Bed Type ID cannot exceed 100 characters.")]
        public string? PartnerBedTypeId { get; set; }

        /// <summary>
        /// Gets or sets the description of the bed type.
        /// </summary>
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(5000, ErrorMessage = "Description cannot exceed 5000 characters.")]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Gets or sets the number of beds of this type available in the room.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Bed Count must be at least 1.")]
        public int BedCount { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated room.
        /// </summary>
        [ForeignKey(nameof(Room))]
        public int? RoomId { get; set; }

        /// <summary>
        /// Gets or sets the room associated with this bed type.
        /// </summary>
        public Room? Room { get; set; }

        /// <summary>
        /// Returns a string representation of the bed type.
        /// </summary>
        /// <returns>A string that represents the current bed type.</returns>
        public override string ToString()
        {
            return $"Bed Type: {Description}, Count: {BedCount}";
        }
    }
}