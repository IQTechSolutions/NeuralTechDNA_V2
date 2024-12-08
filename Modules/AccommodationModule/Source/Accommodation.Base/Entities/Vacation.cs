using System.ComponentModel;
using Filing.Entities;
using NeuralTech.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents a vacation package within the Accommodation system, including details such as pricing, inclusions, and associated amenities.
    /// </summary>
    public class Vacation : ImageFileCollection<Vacation, string>
    {
        /// <summary>
        /// Gets or sets the name of the vacation package.
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(1000, ErrorMessage = "Name cannot exceed 1000 characters.")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the vacation package.
        /// </summary>
        [StringLength(5000, ErrorMessage = "Description cannot exceed 5000 characters.")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the general information related to the vacation package.
        /// </summary>
        [DisplayName("General Information")]
        [StringLength(5000, ErrorMessage = "General Information cannot exceed 5000 characters.")]
        public string? GeneralInformation { get; set; }

        /// <summary>
        /// Gets or sets the start date of the vacation package.
        /// </summary>
        [Required(ErrorMessage = "Start Date is required.")]
        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the vacation package.
        /// </summary>
        [Required(ErrorMessage = "End Date is required.")]
        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        [DateGreaterThan("StartDate", ErrorMessage = "End Date must be later than Start Date.")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the number of rooms available for the vacation package.
        /// </summary>
        [Required(ErrorMessage = "Room Count is required.")]
        [DisplayName("Room Count")]
        [Range(1, int.MaxValue, ErrorMessage = "Room Count must be at least 1.")]
        public int RoomCount { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of bookings allowed for the vacation package.
        /// </summary>
        [Required(ErrorMessage = "Max Booking Count is required.")]
        [DisplayName("Max Booking Days")]
        [Range(1, int.MaxValue, ErrorMessage = "Max Booking Count must be at least 1.")]
        public int MaxBookingCount { get; set; }

        /// <summary>
        /// Gets or sets the booking process details for the vacation package.
        /// </summary>
        [Required(ErrorMessage = "Booking Process is required.")]
        [DisplayName("Booking Process")]
        [StringLength(2000, ErrorMessage = "Booking Process cannot exceed 2000 characters.")]
        public string BookingProcess { get; set; } = null!;

        /// <summary>
        /// Gets or sets the price exclusions for the vacation package.
        /// </summary>
        [Required(ErrorMessage = "Price Exclusions are required.")]
        [DisplayName("Price Exclusions")]
        [StringLength(2000, ErrorMessage = "Price Exclusions cannot exceed 2000 characters.")]
        public string PriceExclusions { get; set; } = null!;

        /// <summary>
        /// Gets or sets the payment terms for the vacation package.
        /// </summary>
        [Required(ErrorMessage = "Payment Terms are required.")]
        [DisplayName("Payment Terms")]
        [StringLength(2000, ErrorMessage = "Payment Terms cannot exceed 2000 characters.")]
        public string PaymentTerms { get; set; } = null!;

        /// <summary>
        /// Gets or sets the foreign key for the associated vacation host.
        /// </summary>
        [ForeignKey(nameof(VacationHost))]
        [DisplayName("Vacation Host ID")]
        [StringLength(100, ErrorMessage = "Vacation Host ID cannot exceed 100 characters.")]
        public string? VacationHostId { get; set; }

        /// <summary>
        /// Gets or sets the vacation host associated with this vacation package.
        /// </summary>
        public VacationHost? VacationHost { get; set; }

        #region Pricing

        /// <summary>
        /// Gets or sets the collection of prices associated with the vacation package.
        /// </summary>
        public ICollection<VacationPrice> Prices { get; set; } = new List<VacationPrice>();

        /// <summary>
        /// Gets or sets the collection of inclusions associated with the vacation package.
        /// </summary>
        public ICollection<Inclusions> Inclusions { get; set; } = new List<Inclusions>();

        /// <summary>
        /// Gets or sets the collection of references associated with the vacation package.
        /// </summary>
        public ICollection<VacationReference> References { get; set; } = new List<VacationReference>();

        /// <summary>
        /// Gets or sets the collection of itineraries associated with the vacation package.
        /// </summary>
        public ICollection<Itinerary> Itineraries { get; set; } = new List<Itinerary>();

        /// <summary>
        /// Gets or sets the collection of highlights associated with the vacation package.
        /// </summary>
        public ICollection<VacationHighlight> VacationHighlights { get; set; } = new List<VacationHighlight>();

        /// <summary>
        /// Gets or sets the collection of destinations associated with the vacation package.
        /// </summary>
        public ICollection<VacationDestination> Destinations { get; set; } = new List<VacationDestination>();

        /// <summary>
        /// Gets or sets the collection of lodgings associated with the vacation package.
        /// </summary>
        public ICollection<VacationLodging> Lodgings { get; set; } = new List<VacationLodging>();

        /// <summary>
        /// Gets or sets the collection of golf courses associated with the vacation package.
        /// </summary>
        public ICollection<VacationGolfCourse> GolfCourses { get; set; } = new List<VacationGolfCourse>();

        #endregion

        /// <summary>
        /// Returns a string representation of the vacation package.
        /// </summary>
        /// <returns>A string that represents the current vacation package.</returns>
        public override string ToString()
        {
            return $"Vacation: {Name}";
        }
    }
}
