using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuralTech.Entities;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents an itinerary within the Accommodation system.
    /// An itinerary consists of multiple itinerary items detailing the schedule for a vacation package.
    /// </summary>
    public class Itinerary : EntityBase<string>
    {
        /// <summary>
        /// Gets or sets the date of the itinerary.
        /// </summary>
        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Itinerary Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the name of the itinerary.
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(1000, ErrorMessage = "Name cannot exceed 1000 characters.")]
        [Display(Name = "Itinerary Name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the foreign key identifier for the associated vacation package.
        /// </summary>
        [ForeignKey(nameof(Vacation))]
        [StringLength(100, ErrorMessage = "Vacation ID cannot exceed 100 characters.")]
        [Display(Name = "Vacation ID")]
        public string? VacationId { get; set; }

        /// <summary>
        /// Gets or sets the vacation package associated with this itinerary.
        /// </summary>
        public Vacation? Vacation { get; set; }

        /// <summary>
        /// Gets or sets the collection of itinerary items associated with this itinerary.
        /// </summary>
        [Display(Name = "Itinerary Details")]
        public ICollection<ItineraryItem> ItineraryDetails { get; set; } = new List<ItineraryItem>();

        /// <summary>
        /// Returns a string representation of the Itinerary.
        /// </summary>
        /// <returns>A string that represents the current Itinerary.</returns>
        public override string ToString()
        {
            return $"Itinerary: {Name} on {Date:yyyy-MM-dd}";
        }
    }
}
