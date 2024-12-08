using NeuralTech.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents a highlight associated with a vacation package in the Accommodation system.
    /// This class includes details such as the highlight's name, description, and the associated vacation package.
    /// </summary>
    public class VacationHighlight : EntityBase<string>
    {
        /// <summary>
        /// Gets or sets the name of the vacation highlight.
        /// </summary>
        [Required(ErrorMessage = "Highlight name is required.")]
        [StringLength(1000, ErrorMessage = "Highlight name cannot exceed 1000 characters.")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the vacation highlight.
        /// </summary>
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(5000, ErrorMessage = "Description cannot exceed 5000 characters.")]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Gets or sets the foreign key for the associated vacation package.
        /// </summary>
        [ForeignKey(nameof(Vacation))]
        [Required(ErrorMessage = "VacationId is required.")]
        [StringLength(100, ErrorMessage = "Vacation ID cannot exceed 100 characters.")]
        public string VacationId { get; set; }

        /// <summary>
        /// Gets or sets the vacation package associated with this highlight.
        /// </summary>
        public Vacation? Vacation { get; set; }

        /// <summary>
        /// Returns a string representation of the vacation highlight.
        /// </summary>
        /// <returns>A string that represents the current vacation highlight.</returns>
        public override string ToString()
        {
            return $"Vacation Highlight: {Name}";
        }
    }
}