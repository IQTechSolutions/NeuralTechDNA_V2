using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuralTech.Entities;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents a package within the Accommodation system.
    /// A package groups multiple rooms and bookings, providing a comprehensive offer to customers.
    /// </summary>
    public class Package : EntityBase<int>
    {
        #region Descriptions

        /// <summary>
        /// Gets or sets a short description of the package.
        /// </summary>
        [StringLength(500, ErrorMessage = "Short Description cannot exceed 500 characters.")]
        [DisplayName("Short Description")]
        public string? ShortDescription { get; set; }

        /// <summary>
        /// Gets or sets the long description of the package.
        /// </summary>
        [Required(ErrorMessage = "Long Description is required.")]
        [StringLength(5000, ErrorMessage = "Long Description cannot exceed 5000 characters.")]
        [DisplayName("Description")]
        public string LongDescription { get; set; } = string.Empty;

        #endregion

        #region Status

        /// <summary>
        /// Gets or sets a value indicating whether the package is deleted.
        /// Soft deletion strategy is used to preserve package data.
        /// </summary>
        public bool Deleted { get; set; } = false;

        #endregion

        #region Partner Information

        /// <summary>
        /// Gets or sets the unique identifier of the available partner associated with the package.
        /// </summary>
        [StringLength(100, ErrorMessage = "Available Partner UID cannot exceed 100 characters.")]
        [DisplayName("Available Partner NBID")]
        public string? AvailablePartnerUid { get; set; }

        /// <summary>
        /// Gets or sets the special rate identifier associated with the package.
        /// </summary>
        [Required(ErrorMessage = "Special Rate ID is required.")]
        [StringLength(100, ErrorMessage = "Special Rate ID cannot exceed 100 characters.")]
        [DisplayName("Special Rate ID")]
        public string SpecialRateId { get; set; } = string.Empty;

        #endregion

        #region Relationships

        /// <summary>
        /// Gets or sets the foreign key for the associated lodging.
        /// </summary>
        [ForeignKey(nameof(Lodging))]
        [StringLength(100, ErrorMessage = "Lodging ID cannot exceed 100 characters.")]
        [DisplayName("Lodging ID")]
        public string? LodgingId { get; set; }

        /// <summary>
        /// Gets or sets the lodging associated with this package.
        /// </summary>
        public Lodging? Lodging { get; set; }

        #endregion

        #region Collections

        /// <summary>
        /// Gets or sets the collection of rooms included in the package.
        /// </summary>
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

        /// <summary>
        /// Gets or sets the collection of bookings associated with the package.
        /// </summary>
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        #endregion
    }
}
