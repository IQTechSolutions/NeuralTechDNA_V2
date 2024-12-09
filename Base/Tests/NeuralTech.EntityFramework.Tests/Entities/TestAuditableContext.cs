using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NeuralTech.EntityFramework.Context;

namespace NeuralTech.EntityFramework.Tests.Entities
{
    /// <summary>
    /// Test implementation of <see cref="AuditableContext"/> with a Products DbSet for testing purposes.
    /// </summary>
    public class TestAuditableContext : AuditableContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestAuditableContext"/> class with specified options and HTTP context accessor.
        /// </summary>
        /// <param name="options">Options for configuring the context.</param>
        /// <param name="httpContextAccessor">Accessor for the current HTTP context.</param>
        public TestAuditableContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor)
            : base(options, httpContextAccessor)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet of Product entities.
        /// </summary>
        public DbSet<Product> Products { get; set; } = null!;
    }
}
