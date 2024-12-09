using NeuralTech.Entities;
using NeuralTech.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeuralTech.EntityFramework.Tests.Entities
{

    /// <summary>
    /// Sample product entity used for testing, implements <see cref="IAuditableEntity"/>.
    /// </summary>
    public class Product : EntityBase<string>, IAuditableEntity
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }
    }
}
