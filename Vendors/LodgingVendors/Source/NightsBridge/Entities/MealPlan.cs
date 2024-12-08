using System.ComponentModel.DataAnnotations;
using Accommodation.Base.Enums;
using NeuralTech.Entities;

namespace NightsBridge.Entities
{
    /// <summary>
    /// Represents a meal plan associated with a room and rate in the NightsBridge system.
    /// </summary>
    public class MealPlan : EntityBase<string>
    {
        /// <summary>
        /// Gets or sets the ID of the room associated with this meal plan.
        /// </summary>
        [Required(ErrorMessage = "Room ID is required.")]
        public int RoomId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the rate associated with this meal plan.
        /// </summary>
        [Required(ErrorMessage = "Rate ID is required.")]
        public int RateId { get; set; }

        /// <summary>
        /// Gets or sets the meal plan type as defined by the partner system.
        /// </summary>
        [Required(ErrorMessage = "Partner Meal Plan ID is required.")]
        public MealPlanTypes PartnerMealPlanId { get; set; }

        /// <summary>
        /// Gets or sets the description of the meal plan.
        /// </summary>
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Gets or sets a value indicating whether this meal plan is the default option.
        /// </summary>
        public bool Default { get; set; }

        /// <summary>
        /// Gets or sets the rate for the meal plan.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Rate must be a positive value.")]
        public double Rate { get; set; }

        /// <summary>
        /// Gets or sets the original rate for the meal plan before any discounts or adjustments.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Original Rate must be a positive value.")]
        public double OriginalRate { get; set; }

        /// <summary>
        /// Returns a string representation of the meal plan.
        /// </summary>
        /// <returns>A string representation of the meal plan.</returns>
        public override string ToString()
        {
            return $"Meal Plan";
        }
    }
}

