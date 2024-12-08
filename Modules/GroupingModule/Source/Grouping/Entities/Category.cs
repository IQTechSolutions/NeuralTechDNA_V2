using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Filing.Entities;
using NeuralTech.Interfaces;

namespace Grouping.Entities
{
    /// <summary>
    /// Represents a category entity with various properties and relationships.
    /// </summary>
    /// <typeparam name="T">The type of the auditable entity.</typeparam>
    public class Category<T> : ImageFileCollection<Category<T>, string> where T : IAuditableEntity<string>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Category{T}"/> class.
        /// </summary>
        public Category() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Category{T}"/> class with specified parameters.
        /// </summary>
        /// <param name="id">The unique identifier of the category.</param>
        /// <param name="name">The name of the category.</param>
        /// <param name="description">The description of the category.</param>
        /// <param name="active">Indicates whether the category is active.</param>
        /// <param name="featured">Indicates whether the category is featured.</param>
        /// <param name="displayAsSlider">Indicates whether the category should be displayed as a slider item.</param>
        /// <param name="displayAsMenuItem">Indicates whether the category should be displayed in the main menu.</param>
        /// <param name="parentCategoryId">The ID of the parent category, if any.</param>
        public Category(string id, string name, string description, bool active, bool featured, bool displayAsSlider, bool displayAsMenuItem, string? parentCategoryId = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Active = active;
            Featured = featured;
            DisplayAsSliderItem = displayAsSlider;
            DisplayCategoryInMainManu = displayAsMenuItem;
            ParentCategoryId = parentCategoryId;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        [Required(ErrorMessage = "Category Name is required.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the category.
        /// </summary>
        [DataType(DataType.MultilineText)]
        [MaxLength(5000, ErrorMessage = "Maximum length for the Description is 5000 characters.")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the category is active.
        /// </summary>
        public bool Active { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the category is featured.
        /// </summary>
        public bool Featured { get; set; } = true;

        /// <summary>
        /// Gets or sets the web tags associated with the category.
        /// </summary>
        [Display(Name = "Tags")]
        [MaxLength(5000, ErrorMessage = "Maximum length for the Tags is 5000 characters.")]
        public string? WebTags { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the category should be displayed in the main menu.
        /// </summary>
        [Display(Name = "Display in Main Menu")]
        public bool DisplayCategoryInMainManu { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the category should be displayed as a slider item.
        /// </summary>
        [Display(Name = "Display As Slider Item")]
        public bool DisplayAsSliderItem { get; set; } = false;

        /// <summary>
        /// Gets or sets the slogan of the category.
        /// </summary>
        [MaxLength(5000, ErrorMessage = "Maximum length for the Slogan is 5000 characters.")]
        public string? Slogan { get; set; }

        /// <summary>
        /// Gets or sets the sub-slogan of the category.
        /// </summary>
        [Display(Name = "Sub-Slogan")]
        [MaxLength(5000, ErrorMessage = "Maximum length for the Sub-Slogan is 5000 characters.")]
        public string? SubSlogan { get; set; }

        #endregion

        #region Parent Category

        /// <summary>
        /// Gets or sets the ID of the parent category.
        /// </summary>
        [ForeignKey(nameof(ParentCategory))]
        public string? ParentCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the parent category.
        /// </summary>
        public Category<T>? ParentCategory { get; set; }

        #endregion

        #region Collection

        /// <summary>
        /// Gets or sets the collection of sub-categories.
        /// </summary>
        public virtual ICollection<Category<T>> SubCategories { get; set; } = new List<Category<T>>();

        /// <summary>
        /// Gets or sets the collection of entities associated with the category.
        /// </summary>
        public virtual ICollection<EntityCategory<T>> EntityCollection { get; set; } = new List<EntityCategory<T>>();

        #endregion

        #region ReadOnly

        /// <summary>
        /// Gets a value indicating whether the category has sub-categories.
        /// </summary>
        public bool HasSubCategories => SubCategories != null && SubCategories.Any();

        #endregion
    }
}

