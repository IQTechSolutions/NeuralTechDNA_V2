using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightsBridge.Entities
{
    /// <summary>
    /// Represents meal plan information associated with a room type in the NightsBridge API V5.
    /// Contains identifiers and descriptions of the meal plans available for a specific room type.
    /// </summary>
    public class RoomTypeMealPlanInfo
    {
        /// <summary>
        /// Gets or sets the unique identifier of the meal plan.
        /// This ID corresponds to a specific meal plan offered by the establishment (e.g., breakfast included).
        /// </summary>
        [JsonProperty("mealplanid")]
        public int MealPlanId { get; set; }

        /// <summary>
        /// Gets or sets the description of the meal plan.
        /// Provides details about what the meal plan includes (e.g., "Bed and Breakfast", "Half Board", "All Inclusive").
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
