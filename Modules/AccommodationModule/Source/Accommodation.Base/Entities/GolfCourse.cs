namespace Accommodation.Base.Entities
{
    using System.ComponentModel.DataAnnotations;
    using Filing.Entities;

    namespace Accommodation.Base.Entities
    {
        /// <summary>
        /// Represents a golf course within the Accommodation system.
        /// </summary>
        public class GolfCourse : ImageFileCollection<GolfCourse, string>
        {
            /// <summary>
            /// Gets or sets the name of the golf course.
            /// </summary>
            [Required(ErrorMessage = "Name is required.")]
            [MaxLength(1000, ErrorMessage = "Name cannot exceed 1000 characters.")]
            public string Name { get; set; } = null!;

            /// <summary>
            /// Gets or sets the description of the golf course.
            /// </summary>
            [Required(ErrorMessage = "Description is required.")]
            [MaxLength(5000, ErrorMessage = "Description cannot exceed 5000 characters.")]
            public string Description { get; set; } = null!;

            /// <summary>
            /// Gets or sets the location of the golf course.
            /// </summary>
            [MaxLength(1000, ErrorMessage = "Location cannot exceed 1000 characters.")]
            public string? Location { get; set; }

            /// <summary>
            /// Gets or sets the collection of vacations associated with the golf course.
            /// </summary>
            public ICollection<VacationGolfCourse> Vacations { get; set; } = new List<VacationGolfCourse>();

            /// <summary>
            /// Returns a string representation of the golf course.
            /// </summary>
            /// <returns>A string that represents the current golf course.</returns>
            public override string ToString()
            {
                return $"Golf Course: {Name}";
            }
        }
    }


}
