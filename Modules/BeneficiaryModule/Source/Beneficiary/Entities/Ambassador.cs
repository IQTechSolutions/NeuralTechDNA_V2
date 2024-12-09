using System.ComponentModel.DataAnnotations;
using NeuralTech.Entities;

namespace Beneficiary.Entities
{
    /// <summary>
    /// Represents an ambassador who may refer bookings or other services and earn commissions.
    /// Includes personal information (name, surname), contact details (phone, email),
    /// a commission percentage, and associated beneficiaries.
    /// </summary>
    public class Ambassador : EntityBase<string>
    {
        /// <summary>
        /// Gets or sets the ambassador's first name.
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(500, ErrorMessage = "Name cannot exceed 500 characters.")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ambassador's surname (last name).
        /// </summary>
        [Required(ErrorMessage = "Surname is required.")]
        [StringLength(500, ErrorMessage = "Surname cannot exceed 500 characters.")]
        public string Surname { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ambassador's phone number.
        /// Must be a valid phone format and is required.
        /// </summary>
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [StringLength(50, ErrorMessage = "Phone number cannot exceed 50 characters.")]
        public string PhoneNr { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ambassador's email address.
        /// Must be a valid email format and is required.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(500, ErrorMessage = "Email cannot exceed 500 characters.")]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Gets or sets the commission percentage the ambassador earns (0 to 100).
        /// </summary>
        [Range(0, 100, ErrorMessage = "Commission Percentage must be between 0 and 100.")]
        public double CommissionPercentage { get; set; }

        /// <summary>
        /// Gets or sets the collection of beneficiaries associated with this ambassador.
        /// Each beneficiary may represent an individual or entity benefiting from the ambassador's activity.
        /// </summary>
        public ICollection<Benificiary> Beneficiaries { get; set; } = new List<Benificiary>();
    }
}
