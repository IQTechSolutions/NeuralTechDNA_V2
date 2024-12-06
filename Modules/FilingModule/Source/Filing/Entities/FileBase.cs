using System.ComponentModel.DataAnnotations;
using NeuralTech.Entities;

namespace Filing.Entities;

public abstract class FileBase : EntityBase<string>
{
    #region Constructors

    /// <summary>
    /// Default constructor for serialization or initialization.
    /// </summary>
    protected FileBase() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileBase"/> class.
    /// </summary>
    /// <param name="filename">The original file name.</param>
    /// <param name="type">The MIME type of the file.</param>
    /// <param name="size">The size of the file in bytes.</param>
    protected FileBase(string filename, string type, long size)
    {
        if (string.IsNullOrWhiteSpace(filename)) throw new ArgumentException("Filename cannot be null or empty.", nameof(filename));
        if (string.IsNullOrWhiteSpace(type)) throw new ArgumentException("Content type cannot be null or empty.", nameof(type));
        if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size), "File size must be greater than zero.");

        FileName = GenerateUniqueFileName(filename);
        ContentType = type;
        Size = size;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileBase"/> class with a specified path.
    /// </summary>
    /// <param name="fileName">The original file name.</param>
    /// <param name="type">The MIME type of the file.</param>
    /// <param name="length">The size of the file in bytes.</param>
    /// <param name="path">The relative path to store the file.</param>
    protected FileBase(string fileName, string type, long length, string path)
        : this(fileName, type, length)
    {
        if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("Path cannot be null or empty.", nameof(path));

        RelativePath = Path.Combine(path, FileName);
        FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", RelativePath);
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the display name of the file.
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the unique file name.
    /// </summary>
    [Required]
    public string FileName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the MIME type of the file.
    /// </summary>
    public string? ContentType { get; set; }

    /// <summary>
    /// Gets or sets the size of the file in bytes.
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// Gets or sets the relative path to the file.
    /// </summary>
    public string? RelativePath { get; set; }

    /// <summary>
    /// Gets or sets the absolute folder path where the file is stored.
    /// </summary>
    public string? FolderPath { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Generates a unique file name by appending a GUID to the original name.
    /// </summary>
    /// <param name="filename">The original file name.</param>
    /// <returns>A unique file name.</returns>
    private static string GenerateUniqueFileName(string filename)
    {
        string name = Path.GetFileNameWithoutExtension(filename);
        string extension = Path.GetExtension(filename);
        return $"{name}_{Guid.NewGuid()}{extension}";
    }

    /// <summary>
    /// Gets the full path to the file, including the specified sub-folder.
    /// </summary>
    /// <param name="subFolderPath">An optional sub-folder path.</param>
    /// <returns>The full path to the file.</returns>
    public string GetFullPath(string subFolderPath = "")
    {
        if (string.IsNullOrWhiteSpace(FileName)) throw new InvalidOperationException("FileName is not set.");
        return Path.Combine(subFolderPath, FileName);
    }

    /// <summary>
    /// Gets the relative path to the file.
    /// </summary>
    /// <param name="subFolderPath">An optional sub-folder path.</param>
    /// <returns>The relative path to the file.</returns>
    public string GetRelativePath(string subFolderPath = "")
    {
        if (string.IsNullOrWhiteSpace(RelativePath)) throw new InvalidOperationException("RelativePath is not set.");
        return Path.Combine("/", subFolderPath, RelativePath).Replace("\\", "/");
    }

    #endregion
}