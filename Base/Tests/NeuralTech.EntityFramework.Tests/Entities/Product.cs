using NeuralTech.Entities;
using NeuralTech.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NeuralTech.EntityFramework.Tests.Entities
{
    /// <summary>
    /// A sample product entity implementing IAuditableEntity.
    /// </summary>
    public class Product : EntityBase<string>, IAuditableEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
