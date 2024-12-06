using NeuralTech.Entities;

namespace Identity.Entities
{
    /// <summary>
    /// Represents the application settings for a user.
    /// </summary>
    public class UserAppSettings : EntityBase<string>
    {
        /// <summary>
        /// Gets or sets a value indicating whether to show the user's job title.
        /// </summary>
        public bool ShowJobTitle { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether to show the user's phone number.
        /// </summary>
        public bool ShowPhoneNr { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether to show the user's email address.
        /// </summary>
        public bool ShowEmailAddress { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the user wants to receive notifications.
        /// </summary>
        public bool ReceiveNotifications { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the user wants to receive newsletters.
        /// </summary>
        public bool ReceiveNewsletters { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the user wants to receive messages.
        /// </summary>
        public bool ReceiveMessages { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the user wants to receive emails.
        /// </summary>
        public bool ReceiveEmails { get; set; } = true;

        /// <summary>
        /// Returns a string representation of the user application settings.
        /// </summary>
        /// <returns>A string representation of the user application settings.</returns>
        public override string ToString()
        {
            return "User Application Settings";
        }
    }
}