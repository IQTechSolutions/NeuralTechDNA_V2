using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Filing.Entities;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents an inclusion associated with a vacation package in the Accommodation system.
    /// Inclusions detail additional features or services included in a vacation package.
    /// </summary>
    public class Inclusions : ImageFileCollection<Inclusions, string>
    {
        /// <summary>
        /// Gets or sets the name of the inclusion.
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(1000, ErrorMessage = "Name cannot exceed 1000 characters.")]
        [Display(Name = "Name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the inclusion.
        /// </summary>
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(5000, ErrorMessage = "Description cannot exceed 5000 characters.")]
        [Display(Name = "Description")]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Gets or sets the foreign key identifier for the associated vacation package.
        /// </summary>
        [ForeignKey(nameof(Vacation))]
        [StringLength(100, ErrorMessage = "Vacation ID cannot exceed 100 characters.")]
        [Display(Name = "Vacation ID")]
        public string? VacationId { get; set; }

        /// <summary>
        /// Gets or sets the vacation package associated with this inclusion.
        /// </summary>
        public Vacation? Vacation { get; set; }

        /// <summary>
        /// Returns a string representation of the Inclusions.
        /// </summary>
        /// <returns>A string that represents the current Inclusions.</returns>
        public override string ToString()
        {
            return $"Inclusion: {Name}";
        }
    }
}