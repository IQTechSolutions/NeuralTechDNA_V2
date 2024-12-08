using Newtonsoft.Json;

namespace NightsBridge.Entities
{
    /// <summary>
    /// Represents a cancellation policy as defined by the NightsBridge API V5.
    /// This policy includes a description and a set of rules that dictate the penalties for cancellations.
    /// </summary>
    public class CancellationPolicy
    {
        /// <summary>
        /// Gets or sets the description of the cancellation policy.
        /// This provides a human-readable summary of the policy.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the collection of cancellation rules.
        /// Each rule specifies the penalty that applies if a booking is cancelled within a certain number of days before arrival.
        /// </summary>
        [JsonProperty("cancellationrules")]
        public CancellationRule[] CancellationRules { get; set; }
    }
}