using System.Text;
using Accommodation.Base.Enums;
using Filing.Entities;
using Grouping.Entities;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents a lodging entity, which may be a hotel, guesthouse, or any accommodation type. 
    /// This class provides comprehensive information about the lodging, including:
    /// <list type="bullet">
    /// <item><description>Identification details (e.g., partner-specific ID).</description></item>
    /// <item><description>Descriptive fields such as name, teaser, and general descriptions.</description></item>
    /// <item><description>Policy details including terms, deposit, and cancellation rules.</description></item>
    /// <item><description>Contact information (phone, email, website) and location data (address, coordinates).</description></item>
    /// <item><description>Associated amenities, categories, services, vouchers, and vacation packages.</description></item>
    /// </list>
    /// This class inherits from <see cref="ImageFileCollection{TEntity, TKey}"/> to handle image associations for the lodging.
    /// </summary>
    public class Lodging : ImageFileCollection<Lodging, string>
    {
        #region Lodging Details

        /// <summary>
        /// A unique identifier provided by a partner system. This helps correlate this lodging 
        /// with external data sources (e.g., an external booking engine).
        /// </summary>
        public string? UniquePartnerId { get; set; }

        /// <summary>
        /// The URL for the cover image of the lodging. Defaults to a placeholder image if not provided.
        /// </summary>
        public string CoverImageUrl { get; set; } = "_content/Accomodation.Blazor/images/NoImage.jpg";

        /// <summary>
        /// The name of the lodging (e.g., "Seaside Guesthouse").
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// A detailed description of the lodging, providing general information for potential guests.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Additional information about the rooms, for example, room features or bed configurations.
        /// </summary>
        public string? RoomInformation { get; set; }

        /// <summary>
        /// A short teaser or summary highlighting key selling points of the lodging.
        /// </summary>
        public string? Teaser { get; set; }

        /// <summary>
        /// A list of facilities available at the lodging, often represented as a comma-separated string.
        /// </summary>
        public string? Facilities { get; set; }

        /// <summary>
        /// Information about nearby attractions or points of interest for guests.
        /// </summary>
        public string? Attractions { get; set; }

        #endregion

        /// <summary>
        /// Holds lodging-specific settings, such as API partner data, configurations, or other contextual information.
        /// </summary>
        public LodgingSettings Settings { get; set; } = null!;

        #region Policies

        /// <summary>
        /// The terms and conditions that apply to bookings and stays at this lodging.
        /// May include payment terms, guest responsibilities, and other policies.
        /// </summary>
        public string? TermsAndConditions { get; set; }

        /// <summary>
        /// The deposit policy, if any, required for securing a reservation.
        /// </summary>
        public string? DepositPolicy { get; set; }

        /// <summary>
        /// The minimum age at which a guest is considered no longer a "child" for pricing or admission.
        /// </summary>
        public int LowestGuestAgeCutOff { get; set; }

        /// <summary>
        /// An intermediate age cutoff that might distinguish between different child age brackets or pricing tiers.
        /// </summary>
        public int MiddleGuestAgeCutOff { get; set; }

        /// <summary>
        /// The highest age cutoff considered for distinguishing child rates from adult rates.
        /// Guests older than this age are typically charged at adult rates.
        /// </summary>
        public int HighestGuestAgeCutOff { get; set; }

        /// <summary>
        /// A computed policy string describing cancellation rules. 
        /// It assembles human-readable text from <see cref="CancellationRules"/>.
        /// </summary>
        public string CancellationPolicy
        {
            get
            {
                if (CancellationRules.Count == 0)
                    return string.Empty;

                var returnString = new StringBuilder();

                // Order by the number of days before arrival to present rules in a logical sequence
                foreach (var rule in CancellationRules.OrderBy(c => c.DaysBeforeBookingThatCancellationIsAvailable))
                {
                    returnString.AppendLine($"If cancelling {rule.DaysBeforeBookingThatCancellationIsAvailable} days before arrival, forfeit " +
                        $"{rule.CancellationFormualaType.GetCancellationRuleAbbreviation(rule.CancellationFormualaValue)}");
                }

                return returnString.ToString();
            }
        }

        /// <summary>
        /// A collection of cancellation rules that define penalties or refunds when a reservation is canceled 
        /// a certain number of days before arrival.
        /// </summary>
        public virtual ICollection<CancellationRule> CancellationRules { get; set; } = new List<CancellationRule>();

        #endregion

        #region Pricing

        /// <summary>
        /// The default rate scheme associated with the lodging, 
        /// defining how rates are calculated or presented by default.
        /// </summary>
        public RateScheme DefaultRateScheme { get; set; }

        /// <summary>
        /// The default commission percentage that may be applied to bookings made through certain channels.
        /// </summary>
        public double DefaultCommissionPercentage { get; set; } = 20;

        /// <summary>
        /// The default markup percentage applied on top of the base rate for this lodging.
        /// Useful for setting prices in profit-sharing scenarios.
        /// </summary>
        public double DefaultMarkupPercentage { get; set; } = 4;

        /// <summary>
        /// A discount percentage applied to the lodging's base rate, if any.
        /// </summary>
        public double Discount { get; set; } = 0;

        /// <summary>
        /// The base rate for the lodging. Might represent a default or starting price for bookings.
        /// </summary>
        public double Rate { get; set; } = 0;

        #endregion

        #region Contact Information

        /// <summary>
        /// A textual field (often comma-separated or JSON-structured) representing multiple contact methods or persons.
        /// </summary>
        public string? Contacts { get; set; }

        /// <summary>
        /// A primary phone number for the lodging (e.g., front desk or reception).
        /// </summary>
        public string? PhoneNr { get; set; }

        /// <summary>
        /// A mobile/cell phone number for the lodging, if available.
        /// </summary>
        public string? CellNr { get; set; }

        /// <summary>
        /// An email address through which the lodging can be contacted, used for reservations or inquiries.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// The official website URL of the lodging, if available.
        /// </summary>
        public string? Website { get; set; }

        #endregion

        #region Page Details

        /// <summary>
        /// The title of the web page that displays details about this lodging. Often used for SEO.
        /// </summary>
        public string? PageTitle { get; set; }

        /// <summary>
        /// Meta keywords associated with this lodging, used for SEO and search functionalities.
        /// </summary>
        public string? MetaKeys { get; set; }

        /// <summary>
        /// A meta description tag for SEO purposes, providing a summary of the lodging's offering.
        /// </summary>
        public string? MetaDescription { get; set; }

        #endregion

        #region Location

        /// <summary>
        /// The identifier of the geographical area (e.g., region or district) in which the lodging is located.
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// Additional information about the area, possibly describing its attractions, environment, or significance.
        /// </summary>
        public string? AreaInfo { get; set; }

        /// <summary>
        /// The lodging's physical address.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// The suburb or neighborhood in which the lodging is located.
        /// </summary>
        public string? Suburb { get; set; }

        /// <summary>
        /// The city in which the lodging is located.
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// The latitude coordinate of the lodging's location.
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        /// The longitude coordinate of the lodging's location.
        /// </summary>
        public double Lng { get; set; }

        /// <summary>
        /// The map zoom level, indicating how closely the map should be zoomed in for this lodging.
        /// </summary>
        public int Zoom { get; set; }

        /// <summary>
        /// Directions or guidance on how to reach the lodging from known reference points.
        /// </summary>
        public string? Directions { get; set; }

        /// <summary>
        /// The identifier of the province (or state) in which the lodging resides, if applicable.
        /// </summary>
        public int? ProvinceId { get; set; }

        #endregion

        #region Collections

        /// <summary>
        /// A collection of category associations for this lodging, categorizing it by type or attributes.
        /// </summary>
        public virtual ICollection<EntityCategory<Lodging>> Categories { get; set; } = new List<EntityCategory<Lodging>>();

        /// <summary>
        /// A collection of packages or account types associated with this lodging, possibly defining 
        /// different rate plans or membership tiers.
        /// </summary>
        public virtual ICollection<Package> AccountTypes { get; set; } = new List<Package>();

        /// <summary>
        /// A collection of amenity items associated with this lodging, describing the amenities available either at the lodging level or in specific rooms.
        /// </summary>
        public virtual ICollection<AmenityItem<Lodging, string>> Amenities { get; set; } = new List<AmenityItem<Lodging, string>>();

        /// <summary>
        /// A collection of vouchers or promotional offers available for bookings at this lodging.
        /// </summary>
        public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();

        /// <summary>
        /// A collection of services offered by the lodging (e.g., spa treatments, guided tours).
        /// </summary>
        public virtual ICollection<Service> Services { get; set; } = new List<Service>();

        /// <summary>
        /// A collection of featured images, possibly highlighting different aspects or areas of the lodging.
        /// </summary>
        public virtual ICollection<FeaturedImage> FeaturedImages { get; set; } = new List<FeaturedImage>();

        /// <summary>
        /// A collection of vacation packages or bundles that include this lodging as part of the offering.
        /// </summary>
        public virtual ICollection<VacationLodging> Vacations { get; set; } = new List<VacationLodging>();

        #endregion
    }
}
