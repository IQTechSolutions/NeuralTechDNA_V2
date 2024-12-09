using Beneficiary.Entities;
using Beneficiary.RestApi.Controllers;
using Beneficiary.Services.Implimentation;
using Beneficiary.Services.Interfaces;
using Beneficiary.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using NeuralTech.Entities;
using NeuralTech.EntityFramework.Context;
using NeuralTech.EntityFramework.Interfaces;
using NeuralTech.EntityFramework.Tests.Entities;
using NeuralTech.ResultWrappers;

namespace Beneficiary.RestApi.Tests
{
    public class AmbassadorsControllerTests : IDisposable
    {
        private readonly TestAuditableContext _context;
        private readonly IRepository<Ambassador, string> _repository;
        private readonly IAmbassadorService _service;
        private readonly AmbassadorsController _controller;
        private readonly SqliteConnection _connection;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

        public AmbassadorsControllerTests()
        {
            // Setup a mock IHttpContextAccessor with a simulated user
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            // Setup in-memory SQLite connection
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<AuditableContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new TestAuditableContext(options, _httpContextAccessorMock.Object);
            _context.Database.EnsureCreated();

            _repository = new Repository<Ambassador, string>(_context);
            _service = new AmbassadorService(_repository);
            _controller = new AmbassadorsController(_service);

            // Seed some test data
            SeedData();
        }

        private void SeedData()
        {
            var ambassadors = new List<Ambassador>
            {
                new Ambassador { Id = Guid.NewGuid().ToString(), Name = "John", Surname = "Doe", PhoneNr = "123456", Email = "john@example.com", CommissionPercentage = 10 },
                new Ambassador { Id = Guid.NewGuid().ToString(), Name = "Jane", Surname = "Smith", PhoneNr = "654321", Email = "jane@example.com", CommissionPercentage = 20 }
            };
            _context.Set<Ambassador>().AddRange(ambassadors);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithAmbassadors()
        {
            // Act
            var result = await _controller.GetAll();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var dtoList = Assert.IsAssignableFrom<IEnumerable<AmbassadorDto>>(actionResult.Value);
            Assert.Equal(2, dtoList.Count());
        }

        [Fact]
        public async Task GetPaged_WithNoSearch_ReturnsPagedResult()
        {
            var parameters = new RequestParameters
            {
                PageNr = 1,
                PageSize = 1
            };

            // Act
            var result = await _controller.GetPaged(parameters);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var pagedResult = Assert.IsType<PaginatedResult<AmbassadorDto>>(actionResult.Value);
            Assert.Equal(1, pagedResult.Data.Count);
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
