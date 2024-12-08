using Filing.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents a reference associated with a vacation package in the Accommodation system.
    /// </summary>
    public class VacationReference : ImageFileCollection<VacationReference, string>
    {
        /// <summary>
        /// Gets or sets the name of the vacation reference.
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(1000, ErrorMessage = "Name cannot exceed 1000 characters.")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the vacation reference.
        /// </summary>
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(5000, ErrorMessage = "Description cannot exceed 5000 characters.")]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Gets or sets the foreign key for the associated vacation package.
        /// </summary>
        [ForeignKey(nameof(Vacation))]
        [Required(ErrorMessage = "Vacation ID is required.")]
        [StringLength(100, ErrorMessage = "Vacation ID cannot exceed 100 characters.")]
        public string VacationId { get; set; } = null!;

        /// <summary>
        /// Gets or sets the vacation package associated with this reference.
        /// </summary>
        public Vacation Vacation { get; set; } = null!;

        /// <summary>
        /// Returns a string representation of the vacation reference.
        /// </summary>
        /// <returns>A string that represents the current vacation reference.</returns>
        public override string ToString()
        {
            return $"Vacation Reference: {Name}";
        }
    }
}
