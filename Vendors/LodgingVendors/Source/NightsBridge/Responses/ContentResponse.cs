using Newtonsoft.Json;
using NightsBridge.Entities;

namespace NightsBridge.Responses
{
    /// <summary>
    /// Represents the response content received from the NightsBridge API V5.
    /// Contains detailed information about a property, including its amenities, policies, and room types.
    /// </summary>
    public class ContentResponse
    {
        /// <summary>
        /// The unique identifier of the bed and breakfast or lodging establishment.
        /// </summary>
        [JsonProperty("bbid")]
        public int BbId { get; set; }

        /// <summary>
        /// Indicates whether the establishment is active or not.
        /// </summary>
        [JsonProperty("active")]
        public bool Active { get; set; }

        /// <summary>
        /// The name of the establishment.
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; } = null!;

        /// <summary>
        /// The currency code used by the establishment, e.g., "USD" or "EUR".
        /// </summary>
        [JsonProperty("currencycode")]
        public string CurrencyCode { get; set; } = null!;

        /// <summary>
        /// The child policy details, specifying age limits and rules for child guests.
        /// </summary>
        [JsonProperty("childpolicy")]
        public ChildPolicy ChildPolicy { get; set; } = null!;

        /// <summary>
        /// Contact information for the establishment.
        /// </summary>
        [JsonProperty("contacts")]
        public string Contacts { get; set; } = null!;

        /// <summary>
        /// The primary phone number of the establishment.
        /// </summary>
        [JsonProperty("phoneno")]
        public string PhoneNr { get; set; } = null!;

        /// <summary>
        /// The mobile phone number for the establishment.
        /// </summary>
        [JsonProperty("cellno")]
        public string CellNr { get; set; } = null!;

        /// <summary>
        /// The email address for the establishment.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; } = null!;

        /// <summary>
        /// The website URL of the establishment.
        /// </summary>
        [JsonProperty("website")]
        public string Website { get; set; } = null!;

        /// <summary>
        /// A short teaser or promotional text about the establishment.
        /// </summary>
        [JsonProperty("teaser")]
        public string Teaser { get; set; } = null!;

        /// <summary>
        /// The physical address of the establishment.
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; } = null!;

        /// <summary>
        /// The suburb where the establishment is located.
        /// </summary>
        [JsonProperty("suburb")]
        public string Suburb { get; set; } = null!;

        /// <summary>
        /// The area identifier associated with the establishment's location.
        /// </summary>
        [JsonProperty("areaid")]
        public int AreaId { get; set; }

        /// <summary>
        /// Directions to the establishment from common landmarks or starting points.
        /// </summary>
        [JsonProperty("directions")]
        public string Directions { get; set; } = null!;

        /// <summary>
        /// The latitude coordinate for the establishment's location.
        /// </summary>
        [JsonProperty("lat")]
        public double Lat { get; set; }

        /// <summary>
        /// The longitude coordinate for the establishment's location.
        /// </summary>
        [JsonProperty("lng")]
        public double Lng { get; set; }

        /// <summary>
        /// The zoom level for map displays centered on the establishment.
        /// </summary>
        [JsonProperty("zoom")]
        public int Zoom { get; set; }

        /// <summary>
        /// Terms and conditions associated with bookings or stays at the establishment.
        /// </summary>
        [JsonProperty("conditions")]
        public string Conditions { get; set; } = null!;

        /// <summary>
        /// A general description of the establishment, including features and highlights.
        /// </summary>
        [JsonProperty("generaldescription")]
        public string GeneralDescription { get; set; } = null!;

        /// <summary>
        /// Information about the surrounding area, such as local attractions or amenities.
        /// </summary>
        [JsonProperty("areainfo")]
        public string AreaInfo { get; set; } = null!;

        /// <summary>
        /// Specific attractions or points of interest near the establishment.
        /// </summary>
        [JsonProperty("attractions")]
        public string Attractions { get; set; } = null!;

        /// <summary>
        /// The grading information, such as star ratings or awards.
        /// </summary>
        [JsonProperty("grading")]
        public Grading[] Grading { get; set; } = null!;

        /// <summary>
        /// Amenities provided by the establishment.
        /// </summary>
        [JsonProperty("propertyamenities")]
        public Amenity[] PropertyAmenities { get; set; } = null!;

        /// <summary>
        /// The minimum number of days in advance required for booking.
        /// </summary>
        [JsonProperty("minadvancebookingdays")]
        public int MinAdvanceBookingDays { get; set; }

        /// <summary>
        /// Indicates whether same-day bookings are allowed.
        /// </summary>
        [JsonProperty("allowsameday")]
        public bool AllowSameday { get; set; }

        /// <summary>
        /// The cut-off time for bookings on the same day.
        /// </summary>
        [JsonProperty("cutofftime")]
        public string CutOffTime { get; set; } = null!;

        /// <summary>
        /// The code representing the property type (e.g., hotel, guesthouse).
        /// </summary>
        [JsonProperty("propertytypecode")]
        public string PropertyTypeCode { get; set; } = null!;

        /// <summary>
        /// A description of the property type.
        /// </summary>
        [JsonProperty("propertytypedescription")]
        public string PropertyTypeDescription { get; set; } = null!;

        /// <summary>
        /// Indicates if a one-night stay is refundable.
        /// </summary>
        [JsonProperty("onenightstayrefundable")]
        public bool OneNightStayRefundable { get; set; }

        /// <summary>
        /// The standard check-in time for the establishment.
        /// </summary>
        [JsonProperty("checkintime")]
        public string CheckInTime { get; set; } = null!;

        /// <summary>
        /// The standard check-out time for the establishment.
        /// </summary>
        [JsonProperty("checkouttime")]
        public string CheckoutTime { get; set; } = null!;

        /// <summary>
        /// Indicates whether pets are allowed and any associated policies.
        /// </summary>
        [JsonProperty("allowpet")]
        public string AllowPets { get; set; } = null!;

        /// <summary>
        /// Details about Wi-Fi availability.
        /// </summary>
        [JsonProperty("wifi")]
        public string WiFi { get; set; } = null!;

        /// <summary>
        /// Information about the cost of Wi-Fi, if any.
        /// </summary>
        [JsonProperty("wificost")]
        public string WiFiCost { get; set; } = null!;

        /// <summary>
        /// Parking information, including availability and cost.
        /// </summary>
        [JsonProperty("parking")]
        public string Parking { get; set; } = null!;

        /// <summary>
        /// Indicates whether the establishment shows the cell phone number publicly.
        /// </summary>
        [JsonProperty("showcellphonenumber")]
        public bool ShowCellPhoneNumber { get; set; }

        /// <summary>
        /// Specifies if smoking is allowed on the premises.
        /// </summary>
        [JsonProperty("allowsmoking")]
        public bool AllowSmoking { get; set; }

        /// <summary>
        /// The cancellation policy details for the establishment.
        /// </summary>
        [JsonProperty("cancellationpolicy")]
        public CancellationPolicy CancellationPolicy { get; set; } = null!;

        /// <summary>
        /// An array of room types available at the establishment.
        /// </summary>
        [JsonProperty("roomtypes")]
        public RoomType[] RoomTypes { get; set; } = null!;
    }
}
