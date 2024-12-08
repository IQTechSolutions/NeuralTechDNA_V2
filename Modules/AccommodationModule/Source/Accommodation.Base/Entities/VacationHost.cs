using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Filing.Entities;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents a host entity associated with vacation packages in the Accommodation system.
    /// This class includes details such as the host's name, description, and the collection of vacations they manage.
    /// It inherits from <see cref="ImageFileCollection{VacationHost, string}"/> to handle image associations for the host.
    /// </summary>
    public class VacationHost : ImageFileCollection<VacationHost, string>
    {
        /// <summary>
        /// Gets or sets the name of the vacation host.
        /// </summary>
        [Required(ErrorMessage = "Host name is required.")]
        [StringLength(1000, ErrorMessage = "Host name cannot exceed 1000 characters.")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the vacation host.
        /// </summary>
        [StringLength(5000, ErrorMessage = "Description cannot exceed 5000 characters.")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the collection of vacations managed by this host.
        /// </summary>
        public ICollection<Vacation> Vacations { get; set; } = new List<Vacation>();

        /// <summary>
        /// Returns a string representation of the vacation host.
        /// </summary>
        /// <returns>A string that represents the current vacation host.</returns>
        public override string ToString()
        {
            return $"Vacation Host: {Name}";
        }
    }
}