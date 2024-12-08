using NeuralTech.Attributes;
using NeuralTech.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents a rule defining the policy for children within a room.
    /// </summary>
    public class ChildPolicyRule : EntityBase<string>
    {
        /// <summary>
        /// Gets or sets the minimum age for the child policy rule.
        /// </summary>
        [Required(ErrorMessage = "Minimum age is required.")]
        [Range(0, 100, ErrorMessage = "Minimum age must be between 0 and 100.")]
        public int MinAge { get; set; }

        /// <summary>
        /// Gets or sets the maximum age for the child policy rule.
        /// </summary>
        [Required(ErrorMessage = "Maximum age is required.")]
        [Range(0, 100, ErrorMessage = "Maximum age must be between 0 and 100.")]
        [GreaterThan("MinAge", ErrorMessage = "Maximum age must be greater than Minimum age.")]
        public int MaxAge { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the child is allowed under this policy.
        /// </summary>
        public bool Allowed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a special rate is applied for this child policy.
        /// </summary>
        public bool UseSpecialRate { get; set; }

        /// <summary>
        /// Gets or sets the type of formula used for calculating the child policy.
        /// </summary>
        [Required(ErrorMessage = "Child Policy Formula Type is required.")]
        [StringLength(1, ErrorMessage = "Child Policy Formula Type must be a single character.")]
        public string? ChildPolicyFormualaType { get; set; } = "N";

        /// <summary>
        /// Gets or sets the value associated with the child policy formula.
        /// </summary>
        [Required(ErrorMessage = "Child Policy Formula Value is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Child Policy Formula Value must be a positive number.")]
        public double ChildPolicyFormualaValue { get; set; } = 1;

        /// <summary>
        /// Gets the description of the child policy rule based on the formula type and value.
        /// </summary>
        public string Description
        {
            get
            {
                return ChildPolicyFormualaType.GetChildPolicyAbbreviation(ChildPolicyFormualaValue, MinAge, MaxAge);
            }
        }

        /// <summary>
        /// Gets or sets a custom description for the child policy rule.
        /// </summary>
        [StringLength(500, ErrorMessage = "Custom Description cannot exceed 500 characters.")]
        public string? CustomDescription { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated room.
        /// </summary>
        [ForeignKey(nameof(Room))]
        public int? RoomId { get; set; }

        /// <summary>
        /// Gets or sets the room associated with this child policy rule.
        /// </summary>
        public Room? Room { get; set; }

        /// <summary>
        /// Returns a string representation of the child policy rule.
        /// </summary>
        /// <returns>A string that represents the current child policy rule.</returns>
        public override string ToString()
        {
            return $"Child Policy Rule: {Description}";
        }
    }

    /// <summary>
    /// Extension methods for the ChildPolicyRule class.
    /// </summary>
    public static class ChildPolicyRuleExtensions
    {
        /// <summary>
        /// Generates an abbreviation for the child policy based on the formula type and value.
        /// </summary>
        /// <param name="childPolicyFormualaType">The type of the child policy formula.</param>
        /// <param name="childPolicyFormulaValue">The value of the child policy formula.</param>
        /// <param name="minAge">The minimum age for the policy.</param>
        /// <param name="maxAge">The maximum age for the policy.</param>
        /// <returns>A string abbreviation describing the child policy.</returns>
        public static string GetChildPolicyAbbreviation(this string? childPolicyFormualaType, double childPolicyFormulaValue, int minAge, int maxAge)
        {
            return childPolicyFormualaType switch
            {
                "N" => $"{minAge} - {maxAge} is not allowed",
                "P" => $"{childPolicyFormulaValue}% of total charge",
                "R" => $"{childPolicyFormulaValue:C} (fixed amount)",
                _ => "Invalid policy type",
            };
        }
    }
}
