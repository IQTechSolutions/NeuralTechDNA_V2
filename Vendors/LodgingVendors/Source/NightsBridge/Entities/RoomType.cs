using Newtonsoft.Json;

namespace NightsBridge.Entities
{
    /// <summary>
    /// Represents a room type in the NightsBridge API V5.
    /// Contains detailed information about the room type, including its amenities, bed types, meal plans, and policies.
    /// </summary>
    public class RoomType
    {
        /// <summary>
        /// Gets or sets the unique identifier for the room type.
        /// </summary>
        [JsonProperty("roomtypeid")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the room type.
        /// For example, "Deluxe Suite" or "Standard Room".
        /// </summary>
        [JsonProperty("roomtypename")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the room type.
        /// Provides details about the features and amenities of the room type.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the maximum occupancy of the room type.
        /// Indicates the total number of guests that the room can accommodate.
        /// </summary>
        [JsonProperty("maxoccupancy")]
        public int MaxOccupancy { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of adults that the room type can accommodate.
        /// </summary>
        [JsonProperty("maxadults")]
        public int MaxAdults { get; set; }

        /// <summary>
        /// Gets or sets the total number of rooms of this type available at the establishment.
        /// </summary>
        [JsonProperty("roomcount")]
        public int RoomCount { get; set; }

        /// <summary>
        /// Gets or sets the rate scheme code for the room type.
        /// This code corresponds to a predefined rate scheme in the NightsBridge system.
        /// </summary>
        [JsonProperty("ratescheme")]
        public int RateSchemeCode { get; set; }

        /// <summary>
        /// Gets or sets the description of the rate scheme.
        /// Provides details about the pricing structure for the room type.
        /// </summary>
        [JsonProperty("rateschemedesc")]
        public string RateScheme { get; set; }

        /// <summary>
        /// Gets or sets the total number of beds in the room type.
        /// </summary>
        [JsonProperty("bedcount")]
        public int BedCount { get; set; }

        /// <summary>
        /// Gets or sets the size of the room in square meters.
        /// </summary>
        [JsonProperty("roomsizeinmeters")]
        public int RoomSize { get; set; }

        /// <summary>
        /// Gets or sets the quality rating of the room type.
        /// For example, "Luxury" or "Standard".
        /// </summary>
        [JsonProperty("quality")]
        public string Quality { get; set; }

        /// <summary>
        /// Gets or sets the type of the room.
        /// For example, "Single", "Double", or "Suite".
        /// </summary>
        [JsonProperty("roomtype")]
        public string RoomType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether smoking is allowed in the room.
        /// </summary>
        [JsonProperty("")]
        public bool SmokingAllowed { get; set; }

        /// <summary>
        /// Gets or sets the amenities available in the room type.
        /// </summary>
        [JsonProperty("amenities")]
        public Amenity[] Amenities { get; set; }

        /// <summary>
        /// Gets or sets the bed types available in the room type.
        /// </summary>
        [JsonProperty("bedtypes")]
        public NBBedType[] BedTypes { get; set; }

        /// <summary>
        /// Gets or sets the meal plans available for the room type.
        /// </summary>
        [JsonProperty("roomtypemealplaninfo")]
        public RoomTypeMealPlanInfo[] MealPlans { get; set; }

        /// <summary>
        /// Gets or sets the child policy for the room type.
        /// Defines rules and rates for children based on different age groups and conditions.
        /// </summary>
        [JsonProperty("childpolicy")]
        public ChildRestrictions ChildPolicy { get; set; }

        /// <summary>
        /// Gets or sets the cleaning fee for the room type.
        /// </summary>
        [JsonProperty("cleaningfee")]
        public double CleaningFee { get; set; }

        /// <summary>
        /// Gets or sets the breakage fee for the room type.
        /// </summary>
        [JsonProperty("breakagefee")]
        public double BreakageFee { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the bathroom is shared with other rooms.
        /// </summary>
        [JsonProperty("bathroomshared")]
        public bool SharedBathroom { get; set; }
    }
}
