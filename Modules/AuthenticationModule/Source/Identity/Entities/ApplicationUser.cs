using Identity.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using NeuralTech.Entities;
using NeuralTech.Interfaces;

namespace Identity.Entities
{
    /// <summary>
    /// Represents an application user with extended properties and relationships.
    /// </summary>
    public class ApplicationUser : IdentityUser<string>, IAuditableEntity<string>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUser"/> class.
        /// </summary>
        public ApplicationUser() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUser"/> class with specified details.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <param name="username">The username.</param>
        /// <param name="firstName">The first name of the user.</param>
        /// <param name="lastName">The last name of the user.</param>
        /// <param name="phoneNr">The phone number of the user.</param>
        /// <param name="email">The email of the user.</param>
        /// <param name="companyName">The company name of the user.</param>
        /// <param name="uniqueUrl">The unique URL for the user (optional).</param>
        public ApplicationUser(string id, string username, string firstName, string lastName, string phoneNr, string email, string companyName, string uniqueUrl = "")
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("ID cannot be null or empty.", nameof(id));
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username cannot be null or empty.", nameof(username));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            Id = id;
            UserName = username;
            PhoneNumber = phoneNr;
            Email = email;
            CompanyName = companyName;

            UserInfo = new UserInfo
            {
                UniqueUrl = uniqueUrl,
                FirstName = firstName,
                LastName = lastName
            };

            UserInfo.ContactNumbers.Add(new ContactNumber<UserInfo> { InternationalCode = "+27", Number = phoneNr, Default = true });
            UserInfo.EmailAddresses.Add(new EmailAddress<UserInfo> { Email = email });
        }

        #endregion

        #region Personal Details

        /// <summary>
        /// Gets or sets the user information associated with the user.
        /// </summary>
        public UserInfo UserInfo { get; set; } = new();

        /// <summary>
        /// Gets or sets the job title of the user.
        /// </summary>
        public string? JobTitle { get; set; }

        /// <summary>
        /// Gets or sets the company name of the user.
        /// </summary>
        public string? CompanyName { get; set; }

        #endregion

        #region License Information

        /// <summary>
        /// Gets or sets the number of licenses associated with the user.
        /// </summary>
        public int Licenses { get; set; } = 100;

        #endregion

        #region Tokens

        /// <summary>
        /// Gets or sets the refresh token for the user.
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the refresh token expiry time.
        /// </summary>
        public DateTime RefreshTokenExpiryTime { get; set; }

        #endregion

        #region Registration Status

        /// <summary>
        /// Gets or sets a value indicating whether the user is active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the user is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the registration status of the user.
        /// </summary>
        public RegistrationStatus RegistrationStatus { get; set; } = RegistrationStatus.Pending;

        /// <summary>
        /// Gets or sets the reason for rejection, if any.
        /// </summary>
        public string? ReasonForRejection { get; set; }

        #endregion

        #region Auditable Members

        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns a string representation of the user.
        /// </summary>
        /// <returns>A string representation of the user.</returns>
        public override string ToString()
        {
            return $"User: {UserName}, Email: {Email}";
        }

        #endregion
    }

    /// <summary>
    /// Extension methods for <see cref="ApplicationUser"/>.
    /// </summary>
    public static class ApplicationUserExtensions
    {
        /// <summary>
        /// Determines whether the user's login is approved based on the registration status and configuration.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="configuration">The application configuration.</param>
        /// <returns><c>true</c> if the user login is approved; otherwise, <c>false</c>.</returns>
        public static bool UserLoginApproved(this ApplicationUser user, IConfiguration configuration)
        {
            var approveSection = configuration.GetSection("ApproveRegistrations");
            if (!approveSection.Exists()) return true;

            var approveUserRegistrations = !string.IsNullOrEmpty(approveSection.Value) && bool.Parse(approveSection.Value);
            return !approveUserRegistrations || user.RegistrationStatus == RegistrationStatus.Accepted;
        }
    }
}
