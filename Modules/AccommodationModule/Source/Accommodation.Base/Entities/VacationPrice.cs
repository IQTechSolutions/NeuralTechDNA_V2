using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuralTech.Entities;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents the pricing details associated with a vacation package in the Accommodation system.
    /// </summary>
    public class VacationPrice : EntityBase<string>
    {
        /// <summary>
        /// Gets or sets the name of the vacation price.
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(1000, ErrorMessage = "Name cannot exceed 1000 characters.")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the vacation price.
        /// </summary>
        [StringLength(5000, ErrorMessage = "Description cannot exceed 5000 characters.")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the price amount for the vacation package.
        /// </summary>
        [Required(ErrorMessage = "Price is required.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated vacation package.
        /// </summary>
        [ForeignKey(nameof(Vacation))]
        [Required(ErrorMessage = "Vacation ID is required.")]
        [StringLength(100, ErrorMessage = "Vacation ID cannot exceed 100 characters.")]
        public string VacationId { get; set; } = null!;

        /// <summary>
        /// Gets or sets the vacation package associated with this price.
        /// </summary>
        public Vacation Vacation { get; set; } = null!;

        /// <summary>
        /// Returns a string representation of the vacation price.
        /// </summary>
        /// <returns>A string that represents the current vacation price.</returns>
        public override string ToString()
        {
            return $"Vacation Price: {Name} - {Price:C}";
        }
    }
}
