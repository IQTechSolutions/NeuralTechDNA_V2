using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Accommodation.Base.Entities.Accommodation.Base.Entities;
using NeuralTech.Entities;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents the association between a vacation package and a golf course in the Accommodation system.
    /// </summary>
    public class VacationGolfCourse : EntityBase<string>
    {
        /// <summary>
        /// Gets or sets the foreign key for the associated golf course.
        /// </summary>
        [ForeignKey(nameof(GolfCourse))]
        [Required(ErrorMessage = "Golf Course ID is required.")]
        [DisplayName("Golf Course ID")]
        [StringLength(100, ErrorMessage = "Golf Course ID cannot exceed 100 characters.")]
        public string GolfCourseId { get; set; } = null!;

        /// <summary>
        /// Gets or sets the golf course associated with this vacation package.
        /// </summary>
        [DisplayName("Golf Course")]
        public GolfCourse GolfCourse { get; set; } = null!;

        /// <summary>
        /// Gets or sets the foreign key for the associated vacation package.
        /// </summary>
        [ForeignKey(nameof(Vacation))]
        [Required(ErrorMessage = "Vacation ID is required.")]
        [DisplayName("Vacation ID")]
        [StringLength(100, ErrorMessage = "Vacation ID cannot exceed 100 characters.")]
        public string VacationId { get; set; } = null!;

        /// <summary>
        /// Gets or sets the vacation package associated with this golf course.
        /// </summary>
        [DisplayName("Vacation ID")]
        public Vacation Vacation { get; set; } = null!;

        /// <summary>
        /// Returns a string representation of the VacationGolfCourse association.
        /// </summary>
        /// <returns>A string that represents the current VacationGolfCourse association.</returns>
        public override string ToString()
        {
            return $"VacationGolfCourse: Vacation ID = {VacationId}, Golf Course ID = {GolfCourseId}";
        }
    }
}
