using Newtonsoft.Json;

namespace NightsBridge.Entities
{
    /// <summary>
    /// Represents a cancellation rule as defined by the NightsBridge API V5.
    /// Each rule specifies the penalty that applies if a booking is cancelled within a certain number of days before arrival.
    /// </summary>
    public class CancellationRule
    {
        /// <summary>
        /// The number of days before the arrival date when this cancellation rule applies.
        /// For example, if <c>DaysBefore</c> is 7, this rule applies to cancellations made 7 days or fewer before arrival.
        /// </summary>
        [JsonProperty("daysbefore")]
        public int DaysBefore { get; set; }

        /// <summary>
        /// The type of amount to be charged as a cancellation fee.
        /// Possible values include:
        /// <list type="bullet">
        ///   <item><description><c>"percentage"</c> - Indicates the <c>Amount</c> is a percentage of the booking total.</description></item>
        ///   <item><description><c>"fixed"</c> - Indicates the <c>Amount</c> is a fixed monetary value.</description></item>
        /// </list>
        /// </summary>
        [JsonProperty("amounttype")]
        public string AmountType { get; set; }

        /// <summary>
        /// A human-readable description of the cancellation rule.
        /// Provides details such as the penalty and conditions.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The amount to be charged as a cancellation fee.
        /// The interpretation of this value depends on the <c>AmountType</c>:
        /// <list type="bullet">
        ///   <item><description>If <c>AmountType</c> is <c>"percentage"</c>, this represents a percentage (e.g., 50 for 50%).</description></item>
        ///   <item><description>If <c>AmountType</c> is <c>"fixed"</c>, this represents a fixed monetary amount in the property's currency.</description></item>
        /// </list>
        /// </summary>
        [JsonProperty("amount")]
        public double Amount { get; set; }
    }
}
