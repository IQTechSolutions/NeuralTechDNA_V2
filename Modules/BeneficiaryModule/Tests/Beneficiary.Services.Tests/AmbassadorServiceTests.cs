using Beneficiary.Entities;
using Beneficiary.Services.Implimentation;
using Beneficiary.Services.Interfaces;
using Beneficiary.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using NeuralTech.Entities;
using NeuralTech.EntityFramework.Context;
using NeuralTech.EntityFramework.Interfaces;
using NeuralTech.EntityFramework.Tests.Entities;

namespace Beneficiary.Services.Tests
{
    public class AmbassadorServiceTests : IDisposable
    {
        private readonly TestAuditableContext _context;
        private readonly IRepository<Ambassador, string> _repository;
        private readonly IAmbassadorService _service;
        private readonly SqliteConnection _connection;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

        public AmbassadorServiceTests()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<TestAuditableContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new TestAuditableContext(options, _httpContextAccessorMock.Object);
            _context.Database.EnsureCreated();

            _repository = new Repository<Ambassador, string>(_context);
            _service = new AmbassadorService(_repository);

            SeedData();
        }

        private void SeedData()
        {
            var ambassadors = new List<Ambassador>
            {
                new Ambassador { Id = Guid.NewGuid().ToString(), Name = "Alice", Surname = "Brown", PhoneNr = "111111", Email = "alice@example.com", CommissionPercentage = 5 },
                new Ambassador { Id = Guid.NewGuid().ToString(), Name = "Bob", Surname = "Green", PhoneNr = "222222", Email = "bob@example.com", CommissionPercentage = 15 }
            };
            _context.Set<Ambassador>().AddRange(ambassadors);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllAmbassadorsAsync_ReturnsSuccessWithList()
        {
            // Act
            var result = await _service.GetAllAmbassadorsAsync();

            // Assert
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);
        }

        [Fact]
        public async Task GetAmbassadorByIdAsync_ReturnsSuccessWhenFound()
        {
            var existingId = _context.Set<Ambassador>().First().Id;

            // Act
            var result = await _service.GetAmbassadorByIdAsync(existingId);

            // Assert
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Data);
            Assert.Equal(existingId, result.Data.Id);
        }

        [Fact]
        public async Task GetAmbassadorByIdAsync_ReturnsFailWhenNotFound()
        {
            // Use a random ID not in the database
            var result = await _service.GetAmbassadorByIdAsync(Guid.NewGuid().ToString());

            Assert.False(result.Succeeded);
            Assert.Contains("No ambassador found", result.Messages.FirstOrDefault() ?? string.Empty);
        }

        [Fact]
        public async Task AddAmbassadorAsync_CreatesNewAmbassador()
        {
            var dto = new AmbassadorDto
            {
                Name = "Charlie",
                Surname = "Delta",
                PhoneNr = "333333",
                Email = "charlie@example.com",
                CommissionPercentage = 12
            };

            var result = await _service.AddAmbassadorAsync(dto);

            Assert.True(result.Succeeded);
            Assert.NotNull(result.Data);
            Assert.False(string.IsNullOrEmpty(result.Data.Id));

            // Verify it's in the database
            var ambassadorCount = _context.Set<Ambassador>().Count();
            Assert.Equal(3, ambassadorCount); // We seeded 2, and added 1 more
        }

        [Fact]
        public async Task GetPagedAmbassadorsAsync_ReturnsPaginatedResult()
        {
            var parameters = new RequestParameters(pageNr: 1, pageSize: 1, orderBy: "Name");

            var pagedResult = await _service.GetPagedAmbassadorsAsync(parameters);

            Assert.True(pagedResult.Succeeded);
            Assert.NotNull(pagedResult.Data);
            Assert.Single(pagedResult.Data);
            Assert.Equal(2, pagedResult.TotalCount);
            Assert.Equal(2, pagedResult.TotalPages);
        }

        public void Dispose()
        {
            _context.Dispose();
            _connection.Close();
        }
    }
}
