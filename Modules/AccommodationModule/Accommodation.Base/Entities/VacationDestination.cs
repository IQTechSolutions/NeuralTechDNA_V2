using NeuralTech.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents the association between a vacation package and a destination in the Accommodation system.
    /// This class includes details such as the destination identifier, associated vacation package, and related navigation properties.
    /// </summary>
    public class VacationDestination : EntityBase<string>
    {
        /// <summary>
        /// Gets or sets the foreign key for the associated destination.
        /// </summary>
        [ForeignKey(nameof(Destination))]
        [StringLength(100, ErrorMessage = "Destination ID cannot exceed 100 characters.")]
        public string? DestinationId { get; set; }

        /// <summary>
        /// Gets or sets the destination associated with this vacation package.
        /// </summary>
        public Destination? Destination { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated vacation package.
        /// </summary>
        [ForeignKey(nameof(Vacation))]
        [Required(ErrorMessage = "Vacation ID is required.")]
        [StringLength(100, ErrorMessage = "Vacation ID cannot exceed 100 characters.")]
        public string VacationId { get; set; }

        /// <summary>
        /// Gets or sets the vacation package associated with this destination.
        /// </summary>
        public Vacation? Vacation { get; set; }

        /// <summary>
        /// Returns a string representation of the VacationDestination association.
        /// </summary>
        /// <returns>A string that represents the current VacationDestination association.</returns>
        public override string ToString()
        {
            return $"VacationDestination: Vacation ID = {VacationId}, Destination ID = {DestinationId}";
        }
    }
}