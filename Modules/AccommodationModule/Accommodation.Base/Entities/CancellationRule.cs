using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuralTech.Entities;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents a cancellation rule for a lodging establishment.
    /// Defines the conditions and penalties for cancellations.
    /// </summary>
    public class CancellationRule : EntityBase<int>
    {     
        /// <summary>
        /// Gets or sets the number of days before the booking date when cancellation is available.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Days before booking must be a non-negative number.")]
        public int DaysBeforeBookingThatCancellationIsAvailable { get; set; } = 1;

        /// <summary>
        /// Gets or sets the type of cancellation formula.
        /// </summary>
        [Required(ErrorMessage = "Cancellation formula type is required.")]
        public string CancellationFormulaType { get; set; }

        /// <summary>
        /// Gets or sets the value of the cancellation formula.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Cancellation formula value must be a non-negative number.")]
        public double CancellationFormulaValue { get; set; } = 1;

        /// <summary>
        /// Gets the description of the cancellation rule.
        /// </summary>
        [NotMapped]
        public string Description => CancellationFormulaType.GetCancellationRuleAbbreviation(CancellationFormulaValue);

        /// <summary>
        /// Gets or sets the ID of the lodging associated with this cancellation rule.
        /// </summary>
        [ForeignKey(nameof(Lodging))]
        [Required(ErrorMessage = "Lodging ID is required.")]
        public string LodgingId { get; set; }

        /// <summary>
        /// Gets or sets the lodging associated with this cancellation rule.
        /// </summary>
        public Lodging Lodging { get; set; }

        /// <summary>
        /// Returns a string representation of the cancellation rule.
        /// </summary>
        /// <returns>A string representing the cancellation rule.</returns>
        public override string ToString()
        {
            return $"Cancellation Rule";
        }
    }

    /// <summary>
    /// Provides extension methods for the <see cref="CancellationRule"/> class.
    /// </summary>
    public static class CancellationRuleExtensions
    {
        /// <summary>
        /// Gets the abbreviation for the cancellation rule based on its type and value.
        /// </summary>
        /// <param name="cancellationFormulaType">The type of the cancellation formula.</param>
        /// <param name="cancellationFormulaValue">The value of the cancellation formula.</param>
        /// <returns>A string representing the abbreviation of the cancellation rule.</returns>
        public static string GetCancellationRuleAbbreviation(this string cancellationFormulaType, double cancellationFormulaValue)
        {
            return cancellationFormulaType switch
            {
                "P" => $"{cancellationFormulaValue}% of total charge",
                "D" => $"{cancellationFormulaValue}% of your deposit",
                "N" => $"Amount equal to {cancellationFormulaValue} nights accommodation",
                "A" => cancellationFormulaValue.ToString("C") + " (fixed amount)",
                _ => string.Empty,
            };
        }
    }
}