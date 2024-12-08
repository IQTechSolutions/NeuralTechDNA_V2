using System.ComponentModel.DataAnnotations;
using Filing.Enums;
using NeuralTech.Entities;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Represents a featured image associated with a lodging or room in the Accommodation system.
    /// </summary>
    public class FeaturedImage : EntityBase<string>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FeaturedImage"/> class.
        /// </summary>
        public FeaturedImage() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeaturedImage"/> class with specified parameters.
        /// </summary>
        /// <param name="lodgingId">The ID of the associated lodging.</param>
        /// <param name="roomId">The ID of the associated room.</param>
        /// <param name="imageUrl">The URL of the image.</param>
        /// <param name="imageType">The type of the image.</param>
        public FeaturedImage(string? lodgingId, int? roomId, string imageUrl, UploadType imageType)
        {
            LodgingId = lodgingId;
            RoomId = roomId;
            ImageUrl = imageUrl;
            ImageType = imageType;
        }

        #endregion

        /// <summary>
        /// Gets or sets the ID of the room associated with this featured image.
        /// </summary>
        public int? RoomId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the lodging associated with this featured image.
        /// </summary>
        public string? LodgingId { get; set; }

        /// <summary>
        /// Gets or sets the URL of the image.
        /// </summary>
        [Required(ErrorMessage = "Image URL is required.")]
        [Url(ErrorMessage = "Invalid URL format.")]
        public string ImageUrl { get; set; } = null!;

        /// <summary>
        /// Gets or sets the type of the image.
        /// </summary>
        [Required(ErrorMessage = "Image Type is required.")]
        public UploadType ImageType { get; set; }

        /// <summary>
        /// Returns a string representation of the featured image.
        /// </summary>
        /// <returns>A string that represents the current featured image.</returns>
        public override string ToString()
        {
            return $"Featured Image: {ImageUrl}";
        }
    }
}

