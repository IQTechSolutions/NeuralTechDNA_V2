using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Accommodation.Base.Enums;
using Filing.Entities;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents a room within the Accommodation system.
    /// This class includes detailed information about the room, including type, description, settings, policies, pricing, and associated entities.
    /// </summary>
    public class Room : ImageFileCollection<Room, int>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Room"/> class.
        /// </summary>
        public Room() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Room"/> class by copying properties from an existing room.
        /// </summary>
        /// <param name="room">The room to copy properties from.</param>
        public Room(Room room)
        {
            PartnerRoomTypeId = room.PartnerRoomTypeId;
            Name = room.Name;
            Description = room.Description;
            AdditionalInfo = room.AdditionalInfo;
            DefaultMealPlanId = room.DefaultMealPlanId;
            DefaultBedTypeId = room.DefaultBedTypeId;
            BedCount = room.BedCount;
            RoomCount = room.RoomCount;
            MaxOccupancy = room.MaxOccupancy;
            MaxAdults = room.MaxAdults;
            BookingTerms = room.BookingTerms;
            CancellationPolicy = room.CancellationPolicy;
            ChildPolicyRules = new List<ChildPolicyRule>(room.ChildPolicyRules);
            RateScheme = room.RateScheme;
            Commision = room.Commision;
            MarkUp = room.MarkUp;
            SpecialRate = room.SpecialRate;
            VoucherRate = room.VoucherRate;
            MealPlans = new List<MealPlan>(room.MealPlans);
            BedTypes = new List<BedType>(room.BedTypes);
            Amneties = new List<ServiceAmenity>(room.Amneties);
            FeaturedImages = new List<FeaturedImage>(room.FeaturedImages);
        }

        #endregion

        #region Room Information

        /// <summary>
        /// Gets or sets the partner-specific room type identifier.
        /// </summary>
        public int? PartnerRoomTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the room.
        /// </summary>
        [Required(ErrorMessage = "Room Name is required.")]
        [StringLength(200, ErrorMessage = "Room Name cannot exceed 200 characters.")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the room.
        /// </summary>
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets additional information about the room.
        /// </summary>
        [StringLength(1000, ErrorMessage = "Additional Info cannot exceed 1000 characters.")]
        public string? AdditionalInfo { get; set; }

        #endregion

        #region Settings

        /// <summary>
        /// Gets or sets the number of beds in the room.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Bed Count must be at least 1.")]
        public int BedCount { get; set; }

        /// <summary>
        /// Gets or sets the total number of such rooms available.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Room Count must be at least 1.")]
        public int RoomCount { get; set; }

        /// <summary>
        /// Gets or sets the maximum occupancy for the room.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Max Occupancy must be at least 1.")]
        public int MaxOccupancy { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of adults allowed in the room.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Max Adults must be at least 1.")]
        public int MaxAdults { get; set; }

        #endregion

        #region Policies

        /// <summary>
        /// Gets or sets the booking terms for the room.
        /// </summary>
        [StringLength(1000, ErrorMessage = "Booking Terms cannot exceed 1000 characters.")]
        public string? BookingTerms { get; set; }

        /// <summary>
        /// Gets or sets the cancellation policy for the room.
        /// </summary>
        [StringLength(1000, ErrorMessage = "Cancellation Policy cannot exceed 1000 characters.")]
        public string? CancellationPolicy { get; set; }

        /// <summary>
        /// Gets the compiled description of child policies based on defined rules.
        /// </summary>
        public string ChildPolicyDescription
        {
            get
            {
                if (ChildPolicyRules.Count == 0)
                    return string.Empty;

                var returnString = new StringBuilder();

                foreach (var rule in ChildPolicyRules.OrderBy(c => c.MinAge))
                {
                    string stringToAdd;
                    switch (rule.ChildPolicyFormualaType)
                    {
                        case "N":
                            stringToAdd = $"{rule.MinAge} - {rule.MaxAge} years: Not allowed";
                            break;

                        case "P":
                            stringToAdd = $"{rule.MinAge} - {rule.MaxAge} years: {rule.ChildPolicyFormualaValue}% of total charge";
                            break;

                        case "R":
                            stringToAdd = $"{rule.MinAge} - {rule.MaxAge} years: {rule.ChildPolicyFormualaValue:C} (fixed amount)";
                            break;

                        default:
                            stringToAdd = "Invalid policy type.";
                            break;
                    }

                    returnString.AppendLine(stringToAdd);
                }

                return returnString.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the collection of child policy rules associated with the room.
        /// </summary>
        public ICollection<ChildPolicyRule> ChildPolicyRules { get; set; } = new List<ChildPolicyRule>();

        #endregion

        #region Settings

        /// <summary>
        /// Gets or sets the default bed type identifier for the room.
        /// </summary>
        [StringLength(100, ErrorMessage = "Default Bed Type ID cannot exceed 100 characters.")]
        public string? DefaultBedTypeId { get; set; }

        /// <summary>
        /// Gets or sets the collection of bed types available in the room.
        /// </summary>
        public ICollection<BedType> BedTypes { get; set; } = new List<BedType>();

        /// <summary>
        /// Gets or sets the default meal plan identifier for the room.
        /// </summary>
        [StringLength(100, ErrorMessage = "Default Meal Plan ID cannot exceed 100 characters.")]
        public string? DefaultMealPlanId { get; set; }

        /// <summary>
        /// Gets or sets the collection of meal plans available for the room.
        /// </summary>
        public ICollection<MealPlan> MealPlans { get; set; } = new List<MealPlan>();

        #endregion

        #region Pricing

        /// <summary>
        /// Gets or sets the rate scheme associated with the room.
        /// </summary>
        public RateScheme? RateScheme { get; set; }

        /// <summary>
        /// Gets or sets the commission percentage for the room.
        /// </summary>
        [Range(0.0, 100.0, ErrorMessage = "Commission must be between 0 and 100.")]
        public double Commision { get; set; } = 4;

        /// <summary>
        /// Gets or sets the markup percentage for the room.
        /// </summary>
        [Range(0.0, 100.0, ErrorMessage = "Markup must be between 0 and 100.")]
        public double MarkUp { get; set; } = 20;

        /// <summary>
        /// Gets or sets the special rate for the room.
        /// </summary>
        [Range(0.0, double.MaxValue, ErrorMessage = "Special Rate must be a non-negative value.")]
        public double SpecialRate { get; set; }

        /// <summary>
        /// Gets or sets the voucher rate for the room.
        /// </summary>
        [Range(0.0, double.MaxValue, ErrorMessage = "Voucher Rate must be a non-negative value.")]
        public double VoucherRate { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        /// Gets or sets the foreign key for the associated package.
        /// </summary>
        [ForeignKey(nameof(Package))]
        public int? PackageId { get; set; }

        /// <summary>
        /// Gets or sets the package associated with this room.
        /// </summary>
        public Package? Package { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated voucher.
        /// </summary>
        [ForeignKey(nameof(Voucher))]
        public int? VoucherId { get; set; }

        /// <summary>
        /// Gets or sets the voucher associated with this room.
        /// </summary>
        public Voucher? Voucher { get; set; }

        #endregion

        #region Collections

        /// <summary>
        /// Gets or sets the collection of featured images associated with the room.
        /// </summary>
        public ICollection<FeaturedImage> FeaturedImages { get; set; } = new List<FeaturedImage>();

        /// <summary>
        /// Gets or sets the collection of amenities available in the room.
        /// </summary>
        public ICollection<ServiceAmenity> Amneties { get; set; } = new List<ServiceAmenity>();

        #endregion

        /// <summary>
        /// Returns a string representation of the Room.
        /// </summary>
        /// <returns>A string that represents the current Room.</returns>
        public override string ToString()
        {
            return $"Room: {Name}";
        }
    }
}
