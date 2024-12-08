using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuralTech.Entities;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents the association between a vacation package and a lodging in the Accommodation system.
    /// </summary>
    public class VacationLodging : EntityBase<string>
    {
        /// <summary>
        /// Gets or sets the foreign key for the associated lodging.
        /// </summary>
        [ForeignKey(nameof(Lodging))]
        [Required(ErrorMessage = "Lodging ID is required.")]
        [StringLength(100, ErrorMessage = "Lodging ID cannot exceed 100 characters.")]
        public string LodgingId { get; set; } = null!;

        /// <summary>
        /// Gets or sets the lodging associated with this vacation package.
        /// </summary>
        public Lodging Lodging { get; set; } = null!;

        /// <summary>
        /// Gets or sets the foreign key for the associated vacation package.
        /// </summary>
        [ForeignKey(nameof(Vacation))]
        [Required(ErrorMessage = "Vacation ID is required.")]
        [StringLength(100, ErrorMessage = "Vacation ID cannot exceed 100 characters.")]
        public string VacationId { get; set; } = null!;

        /// <summary>
        /// Gets or sets the vacation package associated with this lodging.
        /// </summary>
        public Vacation Vacation { get; set; } = null!;

        /// <summary>
        /// Returns a string representation of the VacationLodging association.
        /// </summary>
        /// <returns>A string that represents the current VacationLodging association.</returns>
        public override string ToString()
        {
            return $"VacationLodging: Vacation ID = {VacationId}, Lodging ID = {LodgingId}";
        }
    }
}