using Newtonsoft.Json;

namespace NightsBridge.Entities
{
    /// <summary>
    /// Represents the child restrictions and policies for a lodging establishment.
    /// Defines rules and rates for children based on different age groups and conditions.
    /// </summary>
    public class ChildRestrictions
    {
        /// <summary>
        /// A flag indicating the first child policy condition.
        /// This could be a specific code or identifier used internally.
        /// </summary>
        [JsonProperty("childflag1")]
        public string? ChildFlag1 { get; set; }

        /// <summary>
        /// A description of the first child policy rule.
        /// Provides details about the conditions and restrictions for the first child policy.
        /// </summary>
        [JsonProperty("childrule1description")]
        public string ChildRule1Description { get; set; }

        /// <summary>
        /// The rate applied to children under the first child policy.
        /// This rate could be a percentage or a fixed amount, depending on the policy.
        /// </summary>
        [JsonProperty("childrate1")]
        public float ChildRate1 { get; set; }

        /// <summary>
        /// A flag indicating the second child policy condition.
        /// This could be a specific code or identifier used internally.
        /// </summary>
        [JsonProperty("childflag2")]
        public string? ChildFlag2 { get; set; }

        /// <summary>
        /// A description of the second child policy rule.
        /// Provides details about the conditions and restrictions for the second child policy.
        /// </summary>
        [JsonProperty("childrule2description")]
        public string ChildRule2Description { get; set; }

        /// <summary>
        /// The rate applied to children under the second child policy.
        /// This rate could be a percentage or a fixed amount, depending on the policy.
        /// </summary>
        [JsonProperty("childrate2")]
        public float ChildRate2 { get; set; }

        /// <summary>
        /// Indicates whether the first child policy allows children.
        /// </summary>
        [JsonProperty("allowchild1")]
        public string AllowChild1 { get; set; }

        /// <summary>
        /// Indicates whether the second child policy allows children.
        /// </summary>
        [JsonProperty("allowchild2")]
        public string AllowChild2 { get; set; }

        /// <summary>
        /// A general description of the child restrictions and policies.
        /// Provides an overview of the rules and conditions that apply to children.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Indicates whether the child rules apply to the establishment.
        /// </summary>
        [JsonProperty("childrulesapply")]
        public bool Apply { get; set; }

        /// <summary>
        /// A general description of the child restrictions and policies.
        /// This property is an alias for <see cref="Description"/>.
        /// </summary>
        [JsonProperty("general")]
        public string General { get { return Description; } set { Description = value; } }
    }
}

