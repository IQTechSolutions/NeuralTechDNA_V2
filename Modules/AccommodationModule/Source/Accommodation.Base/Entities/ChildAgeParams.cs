using System.ComponentModel;
using NeuralTech.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents the parameters defining age-related policies for children in a service within the Accommodation system.
    /// This class includes details such as the parameter name, type, value, and associated service.
    /// </summary>
    public class ChildAgeParams : EntityBase<int>
    {
        /// <summary>
        /// Gets or sets the name of the child age parameter.
        /// </summary>
        [Required(ErrorMessage = "Parameter name is required.")]
        [StringLength(1000, ErrorMessage = "Parameter name cannot exceed 1000 characters.")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the type of the child age parameter.
        /// </summary>
        [Required(ErrorMessage = "Parameter type is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Parameter type must be a positive integer.")]
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the value associated with the child age parameter.
        /// </summary>
        [Range(0.0, double.MaxValue, ErrorMessage = "Parameter value must be a non-negative number.")]
        public double? Value { get; set; }

        /// <summary>
        /// Gets or sets the unique number identifying the child age parameter.
        /// </summary>
        [Required(ErrorMessage = "Unique number is required.")]
        [DisplayName("Unique Number")]
        [StringLength(100, ErrorMessage = "Unique number cannot exceed 100 characters.")]
        public string UniqueNr { get; set; } = null!;

        /// <summary>
        /// Gets or sets the foreign key for the associated service.
        /// </summary>
        [ForeignKey(nameof(Service))]
        [Required(ErrorMessage = "Service ID is required.")]
        public int ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the service associated with these child age parameters.
        /// </summary>
        [Required(ErrorMessage = "Service is required.")]
        public Service Service { get; set; } = null!;

        /// <summary>
        /// Returns a string representation of the ChildAgeParams.
        /// </summary>
        /// <returns>A string that represents the current ChildAgeParams.</returns>
        public override string ToString()
        {
            return $"ChildAgeParams: Name = {Name}, Type = {Type}, Value = {Value}";
        }
    }
}
