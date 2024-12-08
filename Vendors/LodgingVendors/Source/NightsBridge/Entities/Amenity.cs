using Newtonsoft.Json;

namespace NightsBridge.Entities
{
    /// <summary>
    /// Represents an amenity provided by the establishment in the NightsBridge API V5.
    /// Contains codes and descriptions that identify the amenity.
    /// </summary>
    public class Amenity
    {
        /// <summary>
        /// The secondary code for the amenity, often an OTA (Online Travel Agency) code.
        /// Maps to the same value as <see cref="Code"/>.
        /// </summary>
        [JsonProperty("otaamenitycode")]
        public string SecondaryCode { get { return Code; } set { Code = value; } }

        /// <summary>
        /// The primary code identifying the amenity.
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// A descriptive name of the amenity.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
