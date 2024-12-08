using System.ComponentModel.DataAnnotations;
using Accommodation.Base.Enums;
using NeuralTech.Entities;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents a meal plan associated with a room and rate in the NightsBridge system.
    /// This class includes details about the meal plan type, descriptions, rates, and default status.
    /// </summary>
    public class MealPlan : EntityBase<string>
    {
        /// <summary>
        /// Gets or sets the ID of the room associated with this meal plan.
        /// </summary>
        [Required(ErrorMessage = "Room ID is required.")]
        [Display(Name = "Room ID")]
        public int RoomId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the rate associated with this meal plan.
        /// </summary>
        [Required(ErrorMessage = "Rate ID is required.")]
        [Display(Name = "Rate ID")]
        public int RateId { get; set; }

        /// <summary>
        /// Gets or sets the meal plan type as defined by the partner system.
        /// </summary>
        [Required(ErrorMessage = "Partner Meal Plan ID is required.")]
        [Display(Name = "Partner Meal Plan Type")]
        public MealPlanTypes PartnerMealPlanId { get; set; }

        /// <summary>
        /// Gets or sets the description of the meal plan.
        /// </summary>
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        [Display(Name = "Meal Plan Description")]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Gets or sets a value indicating whether this meal plan is the default option.
        /// </summary>
        [Display(Name = "Is Default Meal Plan")]
        public bool Default { get; set; }

        /// <summary>
        /// Gets or sets the rate for the meal plan.
        /// </summary>
        [Required(ErrorMessage = "Rate is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Rate must be a positive value.")]
        [Display(Name = "Meal Plan Rate")]
        public double Rate { get; set; }

        /// <summary>
        /// Gets or sets the original rate for the meal plan before any discounts or adjustments.
        /// </summary>
        [Required(ErrorMessage = "Original Rate is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Original Rate must be a positive value.")]
        [Display(Name = "Original Meal Plan Rate")]
        public double OriginalRate { get; set; }

        /// <summary>
        /// Returns a string representation of the meal plan.
        /// </summary>
        /// <returns>A string that represents the current meal plan.</returns>
        public override string ToString()
        {
            return $"Meal Plan: {Description} (Rate: {Rate:C}, Original Rate: {OriginalRate:C})";
        }
    }
}
