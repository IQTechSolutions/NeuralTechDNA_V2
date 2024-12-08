namespace Accommodation.Base.Enums
{
    /// <summary>
    /// Represents the rate scheme for pricing accommodation.
    /// </summary>
    public enum RateScheme
    {
        /// <summary>
        /// Pricing is based on the number of people sharing the accommodation.
        /// </summary>
        PerPersonSharing = 1,

        /// <summary>
        /// Pricing is based on a fixed unit price, regardless of the number of people.
        /// </summary>
        UnitPrice = 2
    }

    /// <summary>
    /// Provides extension methods for the <see cref="RateSheme"/> enum.
    /// </summary>
    public static class RateSchemeExtensions
    {
        /// <summary>
        /// Gets the textual representation of the rate scheme.
        /// </summary>
        /// <param name="rateSheme">The rate scheme.</param>
        /// <returns>A string representing the rate scheme.</returns>
        public static string RateSchemeText(this RateScheme rateSheme)
        {
            return rateSheme switch
            {
                RateScheme.PerPersonSharing => "Per Person Sharing",
                RateScheme.UnitPrice => "Per Unit",
                _ => "Unknown Rate Scheme"
            };
        }
    }
}
