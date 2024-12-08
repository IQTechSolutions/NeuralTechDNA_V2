using System.ComponentModel;
using NeuralTech.Attributes;
using NeuralTech.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents a service offered within the Accommodation system.
    /// This class includes details such as location, service names, supplier information, rates, policies, and associated entities.
    /// </summary>
    public class Service : EntityBase<int>
    {
        /// <summary>
        /// Gets or sets the location where the service is offered.
        /// </summary>
        [StringLength(200, ErrorMessage = "Location cannot exceed 200 characters.")]
        public string? Location { get; set; }

        /// <summary>
        /// Gets or sets the official name of the service.
        /// </summary>
        [Required(ErrorMessage = "Service Name is required.")]
        [DisplayName("Service Name")]
        [StringLength(1000, ErrorMessage = "Service Name cannot exceed 1000 characters.")]
        public string? ServiceName { get; set; }

        /// <summary>
        /// Gets or sets the display name of the service.
        /// </summary>
        [Required(ErrorMessage = "Display Name is required.")]
        [DisplayName("Display Name")]
        [StringLength(200, ErrorMessage = "Display Name cannot exceed 200 characters.")]
        public string DisplayName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the supplier identifier for the service.
        /// </summary>
        [StringLength(100, ErrorMessage = "Supplier cannot exceed 100 characters.")]
        public string? Supplier { get; set; }

        /// <summary>
        /// Gets or sets the name of the supplier providing the service.
        /// </summary>
        [StringLength(200, ErrorMessage = "Supplier Name cannot exceed 200 characters.")]
        [DisplayName("Supplier Name")]
        public string? SupplierName { get; set; }

        /// <summary>
        /// Gets or sets the unique code associated with the service.
        /// </summary>
        [StringLength(50, ErrorMessage = "Code cannot exceed 50 characters.")]
        public string? Code { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the service.
        /// </summary>
        [Required(ErrorMessage = "Unique ID is required.")]
        [StringLength(100, ErrorMessage = "Unique ID cannot exceed 100 characters.")]
        public string? UniqueId { get; set; }

        /// <summary>
        /// Gets or sets the commission percentage for the service.
        /// </summary>
        [Range(0.0, 100.0, ErrorMessage = "Commission Percentage must be between 0 and 100.")]
        [DisplayName("Commission Percentage")]
        public double? CommPerc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the service is displayed.
        /// </summary>
        public bool? Display { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the service is published.
        /// </summary>
        [DisplayName("Is Published")]
        public bool? IsPublished { get; set; }

        /// <summary>
        /// Gets or sets the start date of the rate period.
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayName("Rate Period Start")]
        public DateTime? RatePeriodStart { get; set; }

        /// <summary>
        /// Gets or sets the end date of the rate period.
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayName("Rate Period End")]
        [DateGreaterThan("RatePeriodStart", ErrorMessage = "Rate Period End must be later than Rate Period Start.")]
        public DateTime? RatePeriodEnd { get; set; }

        /// <summary>
        /// Gets or sets the markup percentage applied to the service rate.
        /// </summary>
        [Range(0.0, 100.0, ErrorMessage = "Markup Percentage must be between 0 and 100.")]
        [DisplayName("Markup Percentage")]
        public double? MarkupPerc { get; set; }

        /// <summary>
        /// Gets or sets the current rate of the service.
        /// </summary>
        [Range(0.0, double.MaxValue, ErrorMessage = "Current Rate must be a non-negative value.")]
        [DisplayName("Current Rate")]
        public double? CurrentRate { get; set; }

        /// <summary>
        /// Gets or sets the availability status of rooms for the service.
        /// </summary>
        [StringLength(100, ErrorMessage = "Rooms Available description cannot exceed 100 characters.")]
        [DisplayName("Rooms Available")]
        public string? RoomsAvailable { get; set; }

        /// <summary>
        /// Gets or sets the description of the room rate type.
        /// </summary>
        [StringLength(500, ErrorMessage = "Room Rate Type Description cannot exceed 500 characters.")]
        [DisplayName("Room Rate Description")]
        public string? RoomRateTypeDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the service is part of a package.
        /// </summary>
        [DisplayName("Is Package")]
        public bool? IsPackage { get; set; }

        /// <summary>
        /// Gets or sets the rate code associated with the service.
        /// </summary>
        [StringLength(50, ErrorMessage = "Rate Code cannot exceed 50 characters.")]
        [DisplayName("Rate Code")]
        public string? RateCode { get; set; }

        /// <summary>
        /// Gets or sets the includes information for the service.
        /// </summary>
        [StringLength(1000, ErrorMessage = "Includes description cannot exceed 1000 characters.")]
        public string? Includes { get; set; }

        /// <summary>
        /// Gets or sets the excludes information for the service.
        /// </summary>
        [StringLength(1000, ErrorMessage = "Excludes description cannot exceed 1000 characters.")]
        public string? Excludes { get; set; }

        /// <summary>
        /// Gets or sets the room information for the service.
        /// </summary>
        [StringLength(1000, ErrorMessage = "Room Information cannot exceed 1000 characters.")]
        [DisplayName("Room Information")]
        public string? RoomInformation { get; set; }

        /// <summary>
        /// Gets or sets the child policy associated with the service.
        /// </summary>
        [StringLength(1000, ErrorMessage = "Child Policy description cannot exceed 1000 characters.")]
        [DisplayName("Child Policy")]
        public string? ChildPolicy { get; set; }

        /// <summary>
        /// Gets or sets the cancellation policy associated with the service.
        /// </summary>
        [StringLength(1000, ErrorMessage = "Cancellation Policy description cannot exceed 1000 characters.")]
        [DisplayName("Cancellation Policy")]
        public string? CancellationPolicy { get; set; }

        /// <summary>
        /// Gets or sets the booking terms for the service.
        /// </summary>
        [StringLength(1000, ErrorMessage = "Booking Terms cannot exceed 1000 characters.")]
        [DisplayName("Booking Terms")]
        public string? BookingTerms { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use local data for the service.
        /// </summary>
        [DisplayName("Use Local Data")]
        public bool? UseLocalData { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated package.
        /// </summary>
        [ForeignKey(nameof(Package))]
        public int? PackageId { get; set; }

        /// <summary>
        /// Gets or sets the package associated with this service.
        /// </summary>
        public Package? Package { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated lodging.
        /// </summary>
        [ForeignKey(nameof(Lodging))]
        [Required(ErrorMessage = "Lodging ID is required.")]
        [StringLength(100, ErrorMessage = "Lodging ID cannot exceed 100 characters.")]
        public string LodgingId { get; set; } = null!;

        /// <summary>
        /// Gets or sets the lodging associated with this service.
        /// </summary>
        public Lodging Lodging { get; set; } = null!;

        /// <summary>
        /// Gets or sets the foreign key for the associated available partner.
        /// </summary>
        [ForeignKey(nameof(AvailablePartner))]
        public int? AvailablePartnerId { get; set; }

        /// <summary>
        /// Gets or sets the available partner associated with this service.
        /// </summary>
        public AvailablePartner? AvailablePartner { get; set; }

        /// <summary>
        /// Gets or sets the collection of child age parameters associated with this service.
        /// </summary>
        public virtual ICollection<ChildAgeParams> ChildAgeParams { get; set; } = new List<ChildAgeParams>();

        /// <summary>
        /// Gets or sets the collection of rates associated with this service.
        /// </summary>
        public virtual ICollection<Rates> Rates { get; set; } = new List<Rates>();

        /// <summary>
        /// Returns a string representation of the Service.
        /// </summary>
        /// <returns>A string that represents the current Service.</returns>
        public override string ToString()
        {
            return $"Service: {ServiceName ?? DisplayName}";
        }
    }
}
