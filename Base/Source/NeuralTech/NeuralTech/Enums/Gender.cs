namespace NeuralTech.Enums
{
    /// <summary>
    /// Represents the gender of an individual.
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// Gender is unknown or not specified.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Male gender.
        /// </summary>
        Male = 1,

        /// <summary>
        /// Female gender.
        /// </summary>
        Female = 2,

        /// <summary>
        /// Represents all genders.
        /// </summary>
        All = 3
    }

    /// <summary>
    /// Provides extension methods for the <see cref="Gender"/> enum.
    /// </summary>
    public static class GenderExtensions
    {
        /// <summary>
        /// A dictionary mapping string representations of gender to <see cref="Gender"/> enum values.
        /// </summary>
        private static readonly Dictionary<string, Gender> GenderMap = new(StringComparer.OrdinalIgnoreCase)
        {
            { "M", Gender.Male },
            { "SEUN", Gender.Male },
            { "BOY", Gender.Male },
            { "SEUN/BOY", Gender.Male },
            { "F", Gender.Female },
            { "DOGTER", Gender.Female },
            { "GIRL", Gender.Female },
            { "DOGTER/GIRL", Gender.Female }
        };

        /// <summary>
        /// Converts a string representation of gender to the corresponding <see cref="Gender"/> enum value.
        /// </summary>
        /// <param name="gender">The string representation of gender.</param>
        /// <returns>The corresponding <see cref="Gender"/> enum value, or <see cref="Gender.Unknown"/> if unrecognized.</returns>
        public static Gender ToGender(string? gender)
        {
            if (string.IsNullOrWhiteSpace(gender))
            {
                return Gender.Unknown;
            }

            return GenderMap.TryGetValue(gender.Trim(), out var result) ? result : Gender.Unknown;
        }
    }
}
