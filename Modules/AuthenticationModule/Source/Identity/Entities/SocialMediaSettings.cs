using Microsoft.EntityFrameworkCore;
using NeuralTech.Entities;

namespace Identity.Entities
{
    /// <summary>
    /// Represents the social media settings for a user.
    /// </summary>
    [Owned]
    public class SocialMediaSettings : EntityBase<string>
    {
        /// <summary>
        /// Gets or sets the Facebook URL.
        /// </summary>
        public string? FacebookUrl { get; set; }

        /// <summary>
        /// Gets or sets the LinkedIn URL.
        /// </summary>
        public string? LinkedInUrl { get; set; }

        /// <summary>
        /// Gets or sets the Twitter/X URL.
        /// </summary>
        public string? TwitterXUrl { get; set; }

        /// <summary>
        /// Gets or sets the Instagram URL.
        /// </summary>
        public string? InstagramUrl { get; set; }

        /// <summary>
        /// Gets or sets the YouTube URL.
        /// </summary>
        public string? YouTubeUrl { get; set; }

        /// <summary>
        /// Gets or sets the personal website URL.
        /// </summary>
        public string? WebsiteUrl { get; set; }

        /// <summary>
        /// Gets or sets the chatbot URL.
        /// </summary>
        public string? ChatbotUrl { get; set; }

        /// <summary>
        /// Returns a string representation of the social media settings.
        /// </summary>
        /// <returns>A string representation of the social media settings.</returns>
        public override string ToString()
        {
            return "Social Media Settings";
        }
    }
}