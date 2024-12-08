using NeuralTech.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents a partner available for accommodation services within the Accommodation system.
    /// This class includes details such as the partner's name and associated services, account types, and lodgings.
    /// </summary>
    public class AvailablePartner : EntityBase<int>
    {
        /// <summary>
        /// Gets or sets the name of the available partner.
        /// </summary>
        [Required(ErrorMessage = "Partner Name is required.")]
        [DisplayName("Partner Name")]
        [StringLength(200, ErrorMessage = "Partner Name cannot exceed 200 characters.")]
        public string PartnerName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of services offered by this partner.
        /// </summary>
        public ICollection<Service> Services { get; set; } = new List<Service>();

        /// <summary>
        /// Gets or sets the collection of account types associated with this partner.
        /// </summary>
        public ICollection<Package> AccountTypes { get; set; } = new List<Package>();

        /// <summary>
        /// Gets or sets the collection of lodgings managed by this partner.
        /// </summary>
        public ICollection<Lodging> Lodgings { get; set; } = new List<Lodging>();

        /// <summary>
        /// Returns a string representation of the AvailablePartner.
        /// </summary>
        /// <returns>A string that represents the current AvailablePartner.</returns>
        public override string ToString()
        {
            return $"Available Partner: {PartnerName}";
        }
    }
}