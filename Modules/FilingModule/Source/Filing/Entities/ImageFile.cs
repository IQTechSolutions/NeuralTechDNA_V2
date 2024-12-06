using System.ComponentModel.DataAnnotations.Schema;
using Filing.Enums;
using NeuralTech.Interfaces;

namespace Filing.Entities;

public class ImageFile : FileBase
{
    #region Constructors

    /// <summary>
    /// Default constructor for serialization or initialization.
    /// </summary>
    public ImageFile()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageFile"/> class.
    /// </summary>
    /// <param name="filename">The name of the file.</param>
    /// <param name="contentType">The MIME type of the file.</param>
    /// <param name="size">The size of the file in bytes.</param>
    /// <param name="imageType">The type of image (default is Cover).</param>
    public ImageFile(string filename, string contentType, long size, UploadType imageType = UploadType.Cover)
        : base(filename, contentType, size)
    {
        ImageType = imageType;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the type of the image.
    /// </summary>
    public UploadType ImageType { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Returns a string representation of the image file.
    /// </summary>
    /// <returns>A string representation of the image file.</returns>
    public override string ToString()
    {
        return $"Image File: {FileName}, Type: {ImageType}";
    }

    #endregion
}

public class ImageFile<TEntity, TId> : ImageFile where TEntity : IAuditableEntity<TId>
{
    #region Constructors

    /// <summary>
    /// Default constructor for serialization or initialization.
    /// </summary>
    public ImageFile()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageFile{TEntity, TId}"/> class.
    /// </summary>
    /// <param name="filename">The name of the file.</param>
    /// <param name="contentType">The MIME type of the file.</param>
    /// <param name="size">The size of the file in bytes.</param>
    /// <param name="imageType">The type of image (default is Cover).</param>
    public ImageFile(string filename, string contentType, long size, UploadType imageType = UploadType.Cover)
        : base(filename, contentType, size, imageType)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageFile{TEntity, TId}"/> class with entity association.
    /// </summary>
    /// <param name="entityId">The ID of the associated entity.</param>
    /// <param name="relativePath">The relative path to the file.</param>
    /// <param name="folderPath">The folder path where the file is stored.</param>
    /// <param name="displayName">The display name of the file.</param>
    /// <param name="filename">The name of the file.</param>
    /// <param name="contentType">The MIME type of the file.</param>
    /// <param name="size">The size of the file in bytes.</param>
    /// <param name="imageType">The type of image (default is Cover).</param>
    public ImageFile(TId entityId, string relativePath, string folderPath, string displayName, string filename, string contentType, long size, UploadType imageType = UploadType.Cover)
        : base(filename, contentType, size, imageType)
    {
        EntityId = entityId;
        RelativePath = relativePath;
        FolderPath = folderPath;
        DisplayName = displayName;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the ID of the associated entity.
    /// </summary>
    [ForeignKey(nameof(Entity))]
    public TId? EntityId { get; set; }

    /// <summary>
    /// Gets or sets the associated entity.
    /// </summary>
    public TEntity Entity { get; set; } = default!;

    #endregion

    #region Methods

    /// <summary>
    /// Returns a string representation of the image file.
    /// </summary>
    /// <returns>A string representation of the image file.</returns>
    public override string ToString()
    {
        return $"Image File: {FileName}, Entity ID: {EntityId}, Type: {ImageType}";
    }

    #endregion
}