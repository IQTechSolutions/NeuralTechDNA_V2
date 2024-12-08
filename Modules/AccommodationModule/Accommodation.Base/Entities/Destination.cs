using System.ComponentModel.DataAnnotations;
using Filing.Entities;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents a destination within the Accommodation system.
    /// Destinations are locations where vacation packages are offered.
    /// </summary>
    public class Destination : ImageFileCollection<Destination, string>
    {
        /// <summary>
        /// Gets or sets the name of the destination.
        /// </summary>
        [Required(ErrorMessage = "Destination name is required.")]
        [StringLength(1000, ErrorMessage = "Destination name cannot exceed 1000 characters.")]
        [Display(Name = "Name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the destination.
        /// </summary>
        [Required(ErrorMessage = "Destination description is required.")]
        [StringLength(5000, ErrorMessage = "Destination description cannot exceed 5000 characters.")]
        [Display(Name = "Description")]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of vacation packages associated with this destination.
        /// </summary>
        [Display(Name = "Associated Vacations")]
        public ICollection<VacationDestination> Vacations { get; set; } = new List<VacationDestination>();

        /// <summary>
        /// Returns a string representation of the Destination.
        /// </summary>
        /// <returns>A string that represents the current Destination.</returns>
        public override string ToString()
        {
            return $"Destination: {Name}";
        }
    }
}