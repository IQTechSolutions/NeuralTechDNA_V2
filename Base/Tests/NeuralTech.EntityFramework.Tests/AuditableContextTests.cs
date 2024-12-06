using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using NeuralTech.Entities;
using NeuralTech.EntityFramework.Context;
using NeuralTech.Enums;
using NeuralTech.Interfaces;
using Newtonsoft.Json;

namespace NeuralTech.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="AuditableContext"/> class, ensuring that auditing functionality works as expected.
    /// </summary>
    public class AuditableContextTests : IDisposable
    {
        /// <summary>
        /// Mock of <see cref="IHttpContextAccessor"/> to simulate HTTP context in tests.
        /// </summary>
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

        /// <summary>
        /// Simulated user principal for testing authentication scenarios.
        /// </summary>
        private readonly ClaimsPrincipal _user;

        /// <summary>
        /// Database context options configured for in-memory SQLite database.
        /// </summary>
        private readonly DbContextOptions<TestAuditableContext> _dbContextOptions;

        /// <summary>
        /// In-memory SQLite connection shared across tests.
        /// </summary>
        private readonly SqliteConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditableContextTests"/> class.
        /// Sets up the in-memory database and mock HTTP context for testing.
        /// </summary>
        public AuditableContextTests()
        {
            // Setup a mock IHttpContextAccessor with a simulated user
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            _user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "test-user-id")
            }, "TestAuthentication"));

            _httpContextAccessorMock.Setup(a => a.HttpContext)
                .Returns(new DefaultHttpContext { User = _user });

            // Create a shared SQLite in-memory connection
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            // Configure DbContextOptions to use the shared connection
            _dbContextOptions = new DbContextOptionsBuilder<TestAuditableContext>()
                .UseSqlite(_connection)
                .Options;

            // Ensure the database schema is created
            using var context = new TestAuditableContext(_dbContextOptions, _httpContextAccessorMock.Object);
            context.Database.EnsureCreated();
        }

        /// <summary>
        /// Tests that saving a new entity creates an audit record with the correct details.
        /// </summary>
        [Fact]
        public async Task SaveChangesAsync_AddEntity_CreatesAuditRecord()
        {
            // Arrange: Create a new context and ensure the database is ready
            using var context = new TestAuditableContext(_dbContextOptions, _httpContextAccessorMock.Object);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            var product = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Test Product",
                Price = 9.99m
            };

            // Act: Add the product to the context and save changes
            context.Products.Add(product);
            await context.SaveChangesAsync();

            // Assert: Verify that an audit record was created with correct details
            var auditRecords = context.AuditTrails.ToList();
            Assert.Single(auditRecords);

            var audit = auditRecords.First();
            Assert.Equal("test-user-id", audit.UserId);
            Assert.Equal(AuditActionType.Create, audit.ActionType);
            Assert.Equal("Product", audit.TableName);
            Assert.NotNull(audit.NewValues);
            Assert.Null(audit.OldValues);
            Assert.Contains("Name", audit.AffectedColumns);
            Assert.Contains("Price", audit.AffectedColumns);
        }

        /// <summary>
        /// Tests that updating an entity creates an audit record with before and after values.
        /// </summary>
        [Fact]
        public async Task SaveChangesAsync_UpdateEntity_CreatesAuditRecord()
        {
            // Arrange: Add a product to the context
            using var context = new TestAuditableContext(_dbContextOptions, _httpContextAccessorMock.Object);
            var product = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Original Product",
                Price = 9.99m
            };
            context.Products.Add(product);
            await context.SaveChangesAsync();

            // Act: Modify the product and save changes
            product.Name = "Updated Product";
            product.Price = 19.99m;
            context.Products.Update(product);
            await context.SaveChangesAsync();

            // Assert: Verify that an audit record was created with old and new values
            var auditRecords = context.AuditTrails.Where(a => a.ActionType == AuditActionType.Update).ToList();
            Assert.Single(auditRecords);

            var audit = auditRecords.First();
            Assert.Equal("test-user-id", audit.UserId);
            Assert.Equal(AuditActionType.Update, audit.ActionType);
            Assert.Equal("Product", audit.TableName);
            Assert.NotNull(audit.NewValues);
            Assert.NotNull(audit.OldValues);
            Assert.Contains("Name", audit.AffectedColumns);
            Assert.Contains("Price", audit.AffectedColumns);
        }

        /// <summary>
        /// Tests that deleting an entity creates an audit record with the old values.
        /// </summary>
        [Fact]
        public async Task SaveChangesAsync_DeleteEntity_CreatesAuditRecord()
        {
            // Arrange: Add a product to delete later
            var productId = Guid.NewGuid().ToString();

            using (var context = new TestAuditableContext(_dbContextOptions, _httpContextAccessorMock.Object))
            {
                var product = new Product
                {
                    Id = productId,
                    Name = "Product to Delete",
                    Price = 9.99m
                };
                context.Products.Add(product);
                await context.SaveChangesAsync();
            }

            // Act: Delete the product
            using (var context = new TestAuditableContext(_dbContextOptions, _httpContextAccessorMock.Object))
            {
                var productToDelete = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);
                context.Products.Remove(productToDelete);
                await context.SaveChangesAsync();
            }

            // Assert: Verify that an audit record was created with the old values
            using (var context = new TestAuditableContext(_dbContextOptions, _httpContextAccessorMock.Object))
            {
                var auditRecords = context.AuditTrails.Where(a => a.ActionType == AuditActionType.Delete).ToList();
                Assert.Single(auditRecords);

                var audit = auditRecords.First();
                Assert.Equal("test-user-id", audit.UserId);
                Assert.Equal(AuditActionType.Delete, audit.ActionType);
                Assert.Equal("Product", audit.TableName);
                Assert.Null(audit.NewValues);
                Assert.NotNull(audit.OldValues);

                var affectedColumns = JsonConvert.DeserializeObject<List<string>>(audit.AffectedColumns);
                Assert.Contains("Name", affectedColumns);
                Assert.Contains("Price", affectedColumns);
            }
        }

        /// <summary>
        /// Releases resources used by the test class, such as the database connection.
        /// </summary>
        public void Dispose()
        {
            _connection.Close();
        }

        /// <summary>
        /// Tests that when no user is present in the HTTP context, no audit record is created.
        /// </summary>
        [Fact]
        public async Task SaveChangesAsync_WithoutUser_DoesNotCreateAudit()
        {
            // Arrange: Setup a context without a user
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(a => a.HttpContext).Returns(new DefaultHttpContext());

            using var context = new TestAuditableContext(_dbContextOptions, httpContextAccessorMock.Object);
            var product = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Product without User",
                Price = 9.99m
            };

            // Act: Add the product and save changes
            context.Products.Add(product);
            await context.SaveChangesAsync();

            // Assert: Verify that no audit records were created
            var auditRecords = context.AuditTrails.ToList();
            Assert.Empty(auditRecords);
        }
    }

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
