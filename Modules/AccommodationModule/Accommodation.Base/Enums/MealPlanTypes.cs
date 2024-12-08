using System.ComponentModel.DataAnnotations;

namespace Accommodation.Base.Enums
{
    /// <summary>
    /// Represents the different types of meal plans available for accommodation bookings.
    /// </summary>
    public enum MealPlanTypes
    {
        /// <summary>
        /// No meal plan included.
        /// </summary>
        [Display(Name = "None")]
        None = 0,

        /// <summary>
        /// Bed and breakfast included.
        /// </summary>
        [Display(Name = "Bed and Breakfast")]
        BedAndBreakfast = 1,

        /// <summary>
        /// Self-catering facilities provided.
        /// </summary>
        [Display(Name = "Self Catering")]
        SelfCatering = 2,

        /// <summary>
        /// Dinner, bed, and breakfast included.
        /// </summary>
        [Display(Name = "Dinner, Bed and Breakfast")]
        DinnerBedandBreakfast = 3,

        /// <summary>
        /// Full board, including breakfast, lunch, and dinner.
        /// </summary>
        [Display(Name = "Full Board")]
        FullBoard = 4,

        /// <summary>
        /// Room only, no meals included.
        /// </summary>
        [Display(Name = "Room Only")]
        RoomOnly = 5,

        /// <summary>
        /// Camp site facilities provided.
        /// </summary>
        [Display(Name = "Camp Site")]
        CampSite = 6,

        /// <summary>
        /// All-inclusive, including all meals and drinks.
        /// </summary>
        [Display(Name = "All Inclusive")]
        AllInclusive = 7
    }
}
