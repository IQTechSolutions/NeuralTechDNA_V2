using Filing.Entities;
using NeuralTech.Entities;
using NeuralTech.Enums;

namespace Identity.Entities
{
    /// <summary>
    /// Represents detailed information about a user, including personal details, settings, and collections.
    /// </summary>
    public class UserInfo : ImageFileCollection<UserInfo, string>
    {
        #region Personal Details

        /// <summary>
        /// Gets or sets the unique URL associated with the user.
        /// </summary>
        public string? UniqueUrl { get; set; }

        /// <summary>
        /// Gets or sets the title of the user.
        /// </summary>
        public Title Title { get; set; } = Title.Me;

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets other names or middle names of the user.
        /// </summary>
        public string? OtherNames { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets the full name of the user, combining first, middle, and last names.
        /// </summary>
        public string FullName
        {
            get
            {
                return string.Join(" ", new[] { FirstName, OtherNames, LastName }.Where(name => !string.IsNullOrWhiteSpace(name)));
            }
        }

        /// <summary>
        /// Gets or sets the identity number of the user.
        /// </summary>
        public string? IdentityNr { get; set; }

        /// <summary>
        /// Gets or sets the gender of the user.
        /// </summary>
        public Gender Gender { get; set; } = Gender.Unknown;

        /// <summary>
        /// Gets or sets the marital status of the user.
        /// </summary>
        public MaritalStatus MaritalStatus { get; set; } = MaritalStatus.Single;

        /// <summary>
        /// Gets or sets the current mood or status message of the user.
        /// </summary>
        public string? MoodStatus { get; set; }

        /// <summary>
        /// Gets or sets the biography of the user.
        /// </summary>
        public string? Bio { get; set; }

        /// <summary>
        /// Gets or sets the company name associated with the user.
        /// </summary>
        public string? CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the VAT number of the company associated with the user.
        /// </summary>
        public string? VatNr { get; set; }

        /// <summary>
        /// Gets or sets the family group ID associated with the user.
        /// </summary>
        public string? FamilyGroupId { get; set; }

        #endregion

        #region Settings

        /// <summary>
        /// Gets or sets the application settings for the user.
        /// </summary>
        public UserAppSettings? UserAppSettings { get; set; }

        /// <summary>
        /// Gets or sets the social media settings for the user.
        /// </summary>
        public SocialMediaSettings? SocialMedia { get; set; }

        #endregion

        #region Collections

        /// <summary>
        /// Gets or sets the list of contact numbers associated with the user.
        /// </summary>
        public virtual List<ContactNumber<UserInfo>> ContactNumbers { get; set; } = new();

        /// <summary>
        /// Gets or sets the list of email addresses associated with the user.
        /// </summary>
        public virtual List<EmailAddress<UserInfo>> EmailAddresses { get; set; } = new();

        /// <summary>
        /// Gets or sets the list of addresses associated with the user.
        /// </summary>
        public virtual List<Address<UserInfo>> Addresses { get; set; } = new();

        /// <summary>
        /// Gets or sets the list of profile links associated with the user.
        /// </summary>
        public virtual List<ProfileLink<UserInfo>> ProfileLinks { get; set; } = new();

        #endregion

        #region Overrides

        /// <summary>
        /// Returns a string representation of the user info.
        /// </summary>
        /// <returns>A string representation of the user info.</returns>
        public override string ToString()
        {
            return $"User Info: {FullName}, Unique URL: {UniqueUrl}";
        }

        #endregion
    }
}
