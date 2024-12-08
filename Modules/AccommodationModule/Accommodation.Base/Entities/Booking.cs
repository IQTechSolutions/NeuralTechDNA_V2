using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Accommodation.Base.Enums;
using Identity.Entities;
using NeuralTech.Attributes;
using NeuralTech.Entities;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents a booking made by a user, including personal details, booking details, and associated entities.
    /// </summary>
    public class Booking : EntityBase<int>
    {
        /// <summary>
        /// Gets or sets the unique reference number for the booking.
        /// </summary>
        [Required(ErrorMessage = "Booking Reference Number is required.")]
        [DisplayName("Booking Reference Number")]
        [StringLength(100, ErrorMessage = "Booking Reference Number cannot exceed 100 characters.")]
        public string BookingReferenceNr { get; set; } = null!;

        /// <summary>
        /// Gets or sets the name of the person who made the booking.
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(500, ErrorMessage = "Name cannot exceed 500 characters.")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the contact information for the booking.
        /// </summary>
        [StringLength(1000, ErrorMessage = "Contacts cannot exceed 1000 characters.")]
        public string? Contacts { get; set; }

        /// <summary>
        /// Gets or sets the phone number associated with the booking.
        /// </summary>
        [Phone(ErrorMessage = "Invalid Phone Number.")]
        [DisplayName("Phone Number")]
        [StringLength(100, ErrorMessage = "Phone Number cannot exceed 100 characters.")]
        public string? PhoneNr { get; set; }

        /// <summary>
        /// Gets or sets the email address associated with the booking.
        /// </summary>
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the website related to the booking, if any.
        /// </summary>
        [Url(ErrorMessage = "Invalid Website URL.")]
        [StringLength(200, ErrorMessage = "Website URL cannot exceed 200 characters.")]
        public string? Website { get; set; }

        /// <summary>
        /// Gets or sets the address associated with the booking.
        /// </summary>
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string? Adress { get; set; }

        /// <summary>
        /// Gets or sets the latitude coordinate for the booking location.
        /// </summary>
        [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "Invalid Latitude value.")]
        public string? Lat { get; set; }

        /// <summary>
        /// Gets or sets the longitude coordinate for the booking location.
        /// </summary>
        [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "Invalid Longitude value.")]
        public string? Lng { get; set; }

        /// <summary>
        /// Gets or sets the directions or instructions for reaching the booking location.
        /// </summary>
        [StringLength(500, ErrorMessage = "Directions cannot exceed 500 characters.")]
        public string? Directions { get; set; }

        /// <summary>
        /// Gets or sets the payment instructions for the booking.
        /// </summary>
        [StringLength(500, ErrorMessage = "Payment Instructions cannot exceed 500 characters.")]
        [DisplayName("Payment Instructions")]
        public string? PaymentInstructions { get; set; }

        /// <summary>
        /// Gets or sets the start date of the booking.
        /// </summary>
        [Required(ErrorMessage = "Start Date is required.")]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the booking.
        /// </summary>
        [Required(ErrorMessage = "End Date is required.")]
        [DisplayName("End Date")]
        [DateGreaterThan("StartDate", ErrorMessage = "End Date must be later than Start Date.")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the quantity of rooms booked.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Room Quantity must be at least 1.")]
        [DisplayName("Room Qty")]
        public double RoomQty { get; set; }

        /// <summary>
        /// Gets or sets the rate identifier associated with the booking.
        /// </summary>
        [Required(ErrorMessage = "Rate ID is required.")]
        [DisplayName("Rate ID")]
        public int RateId { get; set; }

        /// <summary>
        /// Gets or sets the description of the rate applied to the booking.
        /// </summary>
        [StringLength(500, ErrorMessage = "Rate Description cannot exceed 500 characters.")]
        [DisplayName("Rate Description")]
        public string? RateDescription { get; set; }

        /// <summary>
        /// Gets or sets the number of adults included in the booking.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "There must be at least one adult.")]
        public int Adults { get; set; }

        /// <summary>
        /// Gets or sets the number of children included in the booking.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Number of children cannot be negative.")]
        public int Children { get; set; }

        /// <summary>
        /// Gets or sets the number of infants included in the booking.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Number of infants cannot be negative.")]
        public int Infants { get; set; }

        /// <summary>
        /// Gets or sets the cancellation rule identifier associated with the booking.
        /// </summary>
        [Required(ErrorMessage = "Cancellation ID is required.")]
        [DisplayName("Cancellation ID")]
        public int CancellationId { get; set; }

        private BookingStatus _bookingStatus = BookingStatus.Pending;

        /// <summary>
        /// Gets or sets the current status of the booking.
        /// If the End Date has passed, the status is automatically set to Completed.
        /// </summary>
        [DisplayName("Booking Status")]
        public BookingStatus BookingStatus
        {
            get
            {
                if (EndDate > DateTime.Now)
                    return _bookingStatus;
                return BookingStatus.Completed;
            }
            set
            {
                _bookingStatus = value;
            }
        }

        /// <summary>
        /// Gets or sets the foreign key for the associated room.
        /// </summary>
        [ForeignKey(nameof(Room))]
        [DisplayName("Room ID")]
        public int? RoomId { get; set; }

        /// <summary>
        /// Gets or sets the associated room for the booking.
        /// </summary>
        public Room? Room { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated package.
        /// </summary>
        [ForeignKey(nameof(Package))]
        [DisplayName("Package ID")]
        public int? PackageId { get; set; }

        /// <summary>
        /// Gets or sets the associated package for the booking.
        /// </summary>
        public Package? Package { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated lodging.
        /// </summary>
        [DisplayName("Lodging ID")]
        [ForeignKey(nameof(Lodging))]
        public string? LodgingId { get; set; }

        /// <summary>
        /// Gets or sets the associated lodging for the booking.
        /// </summary>
        public Lodging? Lodging { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated user.
        /// </summary>
        [ForeignKey(nameof(User))]
        [DisplayName("User ID")]
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the user who made the booking.
        /// </summary>
        public ApplicationUser? User { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated order.
        /// </summary>
        [ForeignKey(nameof(Order))]
        [DisplayName("User Number")]
        public string? OrderNr { get; set; }

        /// <summary>
        /// Gets or sets the associated order for the booking.
        /// </summary>
        public Order? Order { get; set; }
    }
}
