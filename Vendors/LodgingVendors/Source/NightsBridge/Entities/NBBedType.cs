using Newtonsoft.Json;

namespace NightsBridge.Entities
{
    /// <summary>
    /// Represents bed type information for a room in the NightsBridge API V5.
    /// Contains details about the type of beds, their count, and associated room number.
    /// </summary>
    public class NBBedType
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NBBedType"/> class.
        /// </summary>
        public NBBedType() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NBBedType"/> class with specified bed type details.
        /// </summary>
        /// <param name="bedTypeId">The code identifying the type of bed.</param>
        /// <param name="description">A description of the bed type.</param>
        /// <param name="bedCount">The number of beds of this type in the room.</param>
        /// <param name="roomNumber">The room number associated with these beds (optional).</param>
        public NBBedType(string bedTypeId, string description, int bedCount, int roomNumber = 0)
        {
            BedTypeId = bedTypeId;
            Description = description;
            BedCount = bedCount;
            RoomNumber = roomNumber;
        }

        #endregion

        /// <summary>
        /// Gets or sets the code identifying the bed type.
        /// This code corresponds to predefined bed type codes in the NightsBridge system.
        /// </summary>
        [JsonProperty("bedtypecode")]
        public string BedTypeId { get; set; }

        /// <summary>
        /// Gets or sets the description of the bed type.
        /// For example, "Queen Bed", "Twin Beds", or "Sofa Bed".
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the number of beds of this type in the room.
        /// </summary>
        [JsonProperty("bedcount")]
        public int BedCount { get; set; }

        /// <summary>
        /// Gets or sets the number of the room associated with these beds.
        /// This is optional and may be zero if not applicable.
        /// </summary>
        [JsonProperty("RoomNumber")]
        public int RoomNumber { get; set; }
    }
}

