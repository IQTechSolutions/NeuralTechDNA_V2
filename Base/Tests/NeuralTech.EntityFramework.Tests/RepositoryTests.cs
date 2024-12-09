using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NeuralTech.EntityFramework.Context;
using NeuralTech.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;
using NeuralTech.EntityFramework.Tests.Entities;

namespace NeuralTech.EntityFramework.Tests
{
    /// <summary>
    /// Contains unit tests for the Repository class using SQLite in-memory database.
    /// </summary>
    public class RepositoryTests : IDisposable
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly SqliteConnection _connection;
        private readonly TestAuditableContext _context;
        private readonly IRepository<Product, string> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryTests"/> class.
        /// Sets up the SQLite in-memory database and the repository for testing.
        /// </summary>
        public RepositoryTests()
        {
            // Setup a mock IHttpContextAccessor with a user
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            // Create an in-memory SQLite connection
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            // Set up DbContextOptions to use the SQLite connection
            var options = new DbContextOptionsBuilder<TestAuditableContext>()
                .UseSqlite(_connection)
                .Options;

            // Initialize the test context
            _context = new TestAuditableContext(options, _httpContextAccessorMock.Object);

            // Ensure the database schema is created
            _context.Database.EnsureCreated();

            // Initialize the repository with the test context
            _repository = new Repository<Product, string>(_context);
        }

        /// <summary>
        /// Disposes of the test context and closes the SQLite connection after tests.
        /// </summary>
        public void Dispose()
        {
            // Dispose of the context and close the connection
            _context.Dispose();
            _connection.Close();
        }

        [Fact]
        public async Task CreateAsync_Should_Add_Entity()
        {
            // Arrange
            // Create a new product with a unique ID
            var product = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Test Product",
                Price = 9.99m
            };

            // Act
            // Add the product to the repository (changes not saved yet)
            var result = await _repository.CreateAsync(product);

            // Save changes to the database
            var saveResult = await _repository.SaveAsync();

            // Assert
            // Verify that the create operation was successful
            Assert.True(result.Succeeded);

            // Verify that the save operation was successful
            Assert.True(saveResult.Succeeded);

            // Verify that the product exists in the database
            var productsInDb = await _context.Products.ToListAsync();
            Assert.Single(productsInDb);
            Assert.Equal(product.Name, productsInDb.First().Name);
        }

        [Fact]
        public async Task UpdateAsync_Should_Modify_Entity()
        {
            // Arrange
            // Create and add a product to the context
            var product = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Original Product",
                Price = 9.99m
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            // Modify the product's name
            product.Name = "Updated Product";

            // Act
            // Update the product in the repository (changes not saved yet)
            var result = await _repository.UpdateAsync(product);

            // Save changes to the database
            var saveResult = await _repository.SaveAsync();

            // Assert
            // Verify that the update operation was successful
            Assert.True(result.Succeeded);

            // Verify that the save operation was successful
            Assert.True(saveResult.Succeeded);

            // Retrieve the updated product from the database
            var updatedProduct = await _context.Products.FindAsync(product.Id);

            // Verify that the product's name was updated
            Assert.Equal("Updated Product", updatedProduct.Name);
        }

        [Fact]
        public async Task DeleteAsync_Should_Remove_Entity()
        {
            // Arrange
            // Create and add a product to the context
            var product = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Product to Delete",
                Price = 9.99m
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            // Act
            // Delete the product using the repository (changes not saved yet)
            var result = await _repository.DeleteAsync(product);

            // Save changes to the database
            var saveResult = await _repository.SaveAsync();

            // Assert
            // Verify that the delete operation was successful
            Assert.True(result.Succeeded);

            // Verify that the save operation was successful
            Assert.True(saveResult.Succeeded);

            // Verify that the product was removed from the database
            var productsInDb = await _context.Products.ToListAsync();
            Assert.Empty(productsInDb);
        }

        [Fact]
        public async Task DeleteAsync_ById_Should_Remove_Entity()
        {
            // Arrange
            // Create and add a product to the context
            var product = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Product to Delete",
                Price = 9.99m
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            // Act
            // Delete the product by ID using the repository (changes not saved yet)
            var result = await _repository.DeleteAsync(product.Id);

            // Save changes to the database
            var saveResult = await _repository.SaveAsync();

            // Assert
            // Verify that the delete operation was successful
            Assert.True(result.Succeeded);

            // Verify that the save operation was successful
            Assert.True(saveResult.Succeeded);

            // Verify that the product was removed from the database
            var productsInDb = await _context.Products.ToListAsync();
            Assert.Empty(productsInDb);
        }

        [Fact]
        public async Task FindAllAsync_Should_Return_All_Entities()
        {
            // Arrange
            // Add multiple products to the context
            var products = new[]
            {
                new Product { Id = Guid.NewGuid().ToString(), Name = "Product 1", Price = 5.99m },
                new Product { Id = Guid.NewGuid().ToString(), Name = "Product 2", Price = 15.99m }
            };
            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();

            // Act
            // Retrieve all products using the repository
            var result = await _repository.FindAllAsync(trackChanges: false);

            // Assert
            // Verify that the find operation was successful
            Assert.True(result.Succeeded);

            // Verify that all products were retrieved
            Assert.Equal(2, result.Data.Count);

            // Verify the product names
            var productNames = result.Data.Select(p => p.Name).ToList();
            Assert.Contains("Product 1", productNames);
            Assert.Contains("Product 2", productNames);
        }

        [Fact]
        public async Task FindByConditionAsync_Should_Return_Filtered_Entities()
        {
            // Arrange
            // Add multiple products to the context
            var products = new[]
            {
                new Product { Id = Guid.NewGuid().ToString(), Name = "Product 1", Price = 5.99m },
                new Product { Id = Guid.NewGuid().ToString(), Name = "Product 2", Price = 15.99m }
            };
            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();

            // Act
            // Retrieve products with price greater than 10 using the repository
            var result = await _repository.FindByConditionAsync(
                expression: p => p.Price > 10,
                trackChanges: false);

            // Assert
            // Verify that the find operation was successful
            Assert.True(result.Succeeded);

            // Verify that only the correct product was retrieved
            Assert.Single(result.Data);
            Assert.Equal("Product 2", result.Data.First().Name);
        }
    }
}
