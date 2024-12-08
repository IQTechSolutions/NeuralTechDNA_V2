using NeuralTech.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents the rate details associated with a service in the Accommodation system.
    /// This class includes information about single and double room rates, rate codes, and availability.
    /// </summary>
    public class Rates : EntityBase<int>
    {
        /// <summary>
        /// Indicates whether single rooms are available.
        /// </summary>
        public bool? SingleRoom { get; set; }

        /// <summary>
        /// Gets or sets the rate for single rooms.
        /// </summary>
        [Range(0.0, double.MaxValue, ErrorMessage = "Single Room Rate must be a non-negative value.")]
        public double? SingleRoomRate { get; set; }

        /// <summary>
        /// Gets or sets the rate code for single rooms.
        /// </summary>
        [StringLength(50, ErrorMessage = "Rate Code Single cannot exceed 50 characters.")]
        public string? RateCodeSingle { get; set; }

        /// <summary>
        /// Gets or sets additional value information for single rooms.
        /// </summary>
        [StringLength(100, ErrorMessage = "Has Value Single cannot exceed 100 characters.")]
        public string? HasValueSingle { get; set; }

        /// <summary>
        /// Indicates whether double rooms are available.
        /// </summary>
        public bool? DoubleRoom { get; set; }

        /// <summary>
        /// Gets or sets the rate for double rooms.
        /// </summary>
        [Range(0.0, double.MaxValue, ErrorMessage = "Double Room Rate must be a non-negative value.")]
        public double? DoubleRoomRate { get; set; }

        /// <summary>
        /// Gets or sets the rate code for double rooms.
        /// </summary>
        [StringLength(50, ErrorMessage = "Rate Code Double cannot exceed 50 characters.")]
        public string? RateCodeDouble { get; set; }

        /// <summary>
        /// Gets or sets additional value information for double rooms.
        /// </summary>
        [StringLength(100, ErrorMessage = "Has Value Double cannot exceed 100 characters.")]
        public string? HasValueDouble { get; set; }

        /// <summary>
        /// Gets or sets the date when the rates become available.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? AvailableDate { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the rate plan.
        /// </summary>
        [Required(ErrorMessage = "TPN UID is required.")]
        [StringLength(100, ErrorMessage = "TPN UID cannot exceed 100 characters.")]
        public string TpnUid { get; set; } = null!;

        /// <summary>
        /// Gets or sets the foreign key for the associated service.
        /// </summary>
        [ForeignKey(nameof(Service))]
        [Required(ErrorMessage = "Service ID is required.")]
        public int ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the service associated with these rates.
        /// </summary>
        public Service Service { get; set; } = null!;

        /// <summary>
        /// Returns a string representation of the Rates.
        /// </summary>
        /// <returns>A string that represents the current Rates.</returns>
        public override string ToString()
        {
            return $"Rates: TPN UID = {TpnUid}, Service ID = {ServiceId}";
        }
    }
}
