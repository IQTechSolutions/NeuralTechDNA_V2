using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Accommodation.Base.Entities.Accommodation.Base.Entities;
using Accommodation.Base.Entities.Accomodation.Base.Entities;
using NeuralTech.Entities;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents a voucher entity with various properties and relationships.
    /// </summary>
    public class Voucher : EntityBase<int>
    {
        /// <summary>
        /// Gets or sets the title of the voucher.
        /// </summary>
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(500, ErrorMessage = "Title cannot exceed 500 characters.")]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the short description of the voucher.
        /// </summary>
        [Required(ErrorMessage = "Short description is required.")]
        [DisplayName("Short Description")]
        [StringLength(1000, ErrorMessage = "Short Description cannot exceed 1000 characters.")]
        public string ShortDescription { get; set; } = null!;

        /// <summary>
        /// Gets or sets the long description of the voucher.
        /// </summary>
        [Required(ErrorMessage = "Long description is required.")]
        [DisplayName("Description")]
        [StringLength(5000, ErrorMessage = "Description cannot exceed 5000 characters.")]
        public string LongDescription { get; set; } = null!;

        /// <summary>
        /// Gets or sets the rate of the voucher.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Rate must be a positive value.")]
        public double Rate { get; set; }

        /// <summary>
        /// Gets or sets the markup percentage of the voucher.
        /// </summary>
        [Range(0, 100, ErrorMessage = "Markup percentage must be between 0 and 100.")]
        [DisplayName("Markup Percentage")]
        public double MarkupPercentage { get; set; }

        /// <summary>
        /// Gets or sets the commission of the voucher.
        /// </summary>
        [Range(0, 100, ErrorMessage = "Commission must be a positive value.")]
        public double Commission { get; set; }

        /// <summary>
        /// Gets or sets the features of the voucher.
        /// </summary>
        [Required(ErrorMessage = "Features are required.")]
        [StringLength(5000, ErrorMessage = "Features cannot exceed 5000 characters.")]
        public string Features { get; set; } = null!;

        /// <summary>
        /// Gets or sets the terms of the voucher.
        /// </summary>
        [Required(ErrorMessage = "Terms are required.")]
        [StringLength(5000, ErrorMessage = "Description cannot exceed 5000 characters.")]
        public string Terms { get; set; } = null!;

        /// <summary>
        /// Gets or sets a value indicating whether the voucher is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the voucher is featured.
        /// </summary>
        public bool Featured { get; set; }

        /// <summary>
        /// Gets or sets the ID of the associated lodging.
        /// </summary>
        [ForeignKey(nameof(Lodging))]
        [StringLength(100, ErrorMessage = "LodgingId cannot exceed 100 characters.")]
        public string? LodgingId { get; set; }

        /// <summary>
        /// Gets or sets the associated lodging.
        /// </summary>
        public Lodging? Lodging { get; set; }

        /// <summary>
        /// Gets or sets the collection of rooms associated with the voucher.
        /// </summary>
        public ICollection<Room> Rooms { get; set; } = new List<Room>();

        /// <summary>
        /// Gets or sets the collection of user vouchers associated with the voucher.
        /// </summary>
        public ICollection<UserVoucher> Vouchers { get; set; } = new List<UserVoucher>();
    }
}

