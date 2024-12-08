using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuralTech.Entities;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents an item within an itinerary in the Accommodation system.
    /// This class includes details such as the description of the itinerary item and its association with a specific itinerary.
    /// </summary>
    public class ItineraryItem : EntityBase<string>
    {
        /// <summary>
        /// Gets or sets the description of the itinerary item.
        /// </summary>
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters.")]
        [Display(Name = "Itinerary Item Description")]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Gets or sets the foreign key identifier for the associated itinerary.
        /// </summary>
        [ForeignKey(nameof(Itinerary))]
        [StringLength(100, ErrorMessage = "Itinerary ID cannot exceed 100 characters.")]
        [Display(Name = "Itinerary ID")]
        public string? ItineraryId { get; set; }

        /// <summary>
        /// Gets or sets the itinerary associated with this itinerary item.
        /// </summary>
        public Itinerary? Itinerary { get; set; }

        /// <summary>
        /// Returns a string representation of the ItineraryItem.
        /// </summary>
        /// <returns>A string that represents the current ItineraryItem.</returns>
        public override string ToString()
        {
            return $"Itinerary Item: {Description}";
        }
    }
}