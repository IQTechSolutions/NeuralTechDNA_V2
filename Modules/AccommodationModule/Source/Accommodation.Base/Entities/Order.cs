using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Accommodation.Base.Entities.Accommodation.Base.Entities;
using NeuralTech.Entities;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents an order placed by a customer, including personal details and associated bookings and vouchers.
    /// </summary>
    public class Order : EntityBase<string>
    {
        /// <summary>
        /// Gets or sets the first name of the person placing the order.
        /// </summary>
        [Required(ErrorMessage = "First Name is required.")]
        [DisplayName("First Name")]
        [StringLength(100, ErrorMessage = "First Name cannot exceed 100 characters.")]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the last name of the person placing the order.
        /// </summary>
        [Required(ErrorMessage = "Last Name is required.")]
        [DisplayName("Last Name")]
        [StringLength(100, ErrorMessage = "Last Name cannot exceed 100 characters.")]
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the email address of the person placing the order.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Gets or sets the phone number of the person placing the order.
        /// </summary>
        [Required(ErrorMessage = "Phone Number is required.")]
        [DisplayName("Phone Number")]
        [Phone(ErrorMessage = "Invalid Phone Number.")]
        [StringLength(100, ErrorMessage = "Phone Number cannot exceed 100 characters.")]
        public string PhoneNr { get; set; } = null!;

        /// <summary>
        /// Gets or sets the subtotal excluding VAT (Value Added Tax).
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "SubTotalExcl must be a positive value.")]
        [DisplayName("Sub Total (excl)")]
        public double SubTotalExcl { get; set; }

        /// <summary>
        /// Gets or sets the VAT (Value Added Tax) amount.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "VAT must be a positive value.")]
        public double Vat { get; set; }

        /// <summary>
        /// Gets or sets the subtotal including VAT.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "SubTotalIncl must be a positive value.")]
        [DisplayName("Sub Total (incl)")]
        public double SubTotalIncl { get; set; }

        /// <summary>
        /// Gets or sets the discount amount applied to the order.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Discount must be a positive value.")]
        public double Discount { get; set; }

        /// <summary>
        /// Gets or sets the total amount due for the order.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "TotalDue must be a positive value.")]
        [DisplayName("Total Due")]
        public double TotalDue { get; set; }

        /// <summary>
        /// Gets or sets the list of bookings associated with this order.
        /// </summary>
        public List<Booking> Bookings { get; set; } = new List<Booking>();

        /// <summary>
        /// Gets or sets the list of user vouchers associated with this order.
        /// </summary>
        public List<UserVoucher> Vouchers { get; set; } = new List<UserVoucher>();
    }
}
