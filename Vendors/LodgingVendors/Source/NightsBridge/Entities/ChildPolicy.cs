using Newtonsoft.Json;

namespace NightsBridge.Entities
{
    /// <summary>
    /// Represents the child policy details for a lodging establishment.
    /// Defines the age cut-offs used to categorize guests as infants, children, or adults.
    /// </summary>
    public class ChildPolicy
    {
        /// <summary>
        /// The lowest age cut-off for children.
        /// Guests below this age are considered infants.
        /// </summary>
        [JsonProperty("lowerlimit")]
        public int LowestAgeCutOff { get; set; }

        /// <summary>
        /// The middle age cut-off for children.
        /// Guests between the lowest age cut-off and this age are considered young children.
        /// </summary>
        [JsonProperty("childage1")]
        public int MiddleAgeCutOff { get; set; }

        /// <summary>
        /// The highest age cut-off for children.
        /// Guests between the middle age cut-off and this age are considered older children.
        /// Guests above this age are considered adults.
        /// </summary>
        [JsonProperty("childage2")]
        public int HighestAgeCutOff { get; set; }
    }
}   
