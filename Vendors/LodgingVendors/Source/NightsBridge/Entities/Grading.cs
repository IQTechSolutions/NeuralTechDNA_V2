using Newtonsoft.Json;

namespace NightsBridge.Entities
{
    /// <summary>
    /// Represents the grading information of an establishment, such as star ratings or quality assessments
    /// provided by recognized grading authorities.
    /// </summary>
    public class Grading
    {
        /// <summary>
        /// The name of the grading authority that provided the grade.
        /// For example, "Tourism Grading Council of South Africa".
        /// </summary>
        [JsonProperty("gradingauthority")]
        public string GradingAuthority { get; set; }

        /// <summary>
        /// The grade or rating assigned to the establishment by the grading authority.
        /// This could be a star rating (e.g., "5 Star") or a qualitative assessment (e.g., "Luxury").
        /// </summary>
        [JsonProperty("grade")]
        public string Grade { get; set; }
    }
}
