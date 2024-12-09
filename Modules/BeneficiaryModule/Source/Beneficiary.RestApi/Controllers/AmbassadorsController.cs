using Accomodation.Base.Endpoints;
using Beneficiary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Beneficiary.Shared.DataTransferObjects;
using NeuralTech.Entities;
using NeuralTech.ResultWrappers;

namespace Beneficiary.RestApi.Controllers
{
    /// <summary>
    /// Provides a RESTful API for managing Ambassador entities.
    /// Offers endpoints for listing, paging, retrieving single ambassadors, and performing CRUD operations.
    /// Routes are defined in AmbassadorRoutes static class for maintainability.
    /// </summary>
    [ApiController]
    public class AmbassadorsController : ControllerBase
    {
        private readonly IAmbassadorService _ambassadorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AmbassadorsController"/> class.
        /// </summary>
        /// <param name="ambassadorService">The service used to interact with Ambassador data.</param>
        public AmbassadorsController(IAmbassadorService ambassadorService)
        {
            _ambassadorService = ambassadorService;
        }

        /// <summary>
        /// Retrieves all ambassadors without paging.
        /// </summary>
        [HttpGet(AmbassadorRoutes.GetAll)]
        public async Task<ActionResult<IEnumerable<AmbassadorDto>>> GetAll()
        {
            var results = await _ambassadorService.GetAllAmbassadorsAsync();
            return Ok(results);
        }

        /// <summary>
        /// Retrieves a paged list of ambassadors.
        /// </summary>
        /// <param name="parameters">Paging and filtering parameters.</param>
        [HttpGet(AmbassadorRoutes.GetPaged)]
        public async Task<ActionResult<PaginatedResult<AmbassadorDto>>> GetPaged([FromQuery] RequestParameters parameters)
        {
            var pagedResult = await _ambassadorService.GetPagedAmbassadorsAsync(parameters);
            return Ok(pagedResult);
        }

        /// <summary>
        /// Retrieves an ambassador by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the ambassador to retrieve.</param>
        [HttpGet(AmbassadorRoutes.GetById)]
        public async Task<ActionResult<AmbassadorDto>> GetById(string id)
        {
            var ambassador = await _ambassadorService.GetAmbassadorByIdAsync(id);
            if (ambassador == null)
                return NotFound($"No ambassador found with ID {id}.");

            return Ok(ambassador);
        }

        /// <summary>
        /// Creates a new ambassador.
        /// </summary>
        /// <param name="dto">The DTO representing the ambassador data.</param>
        [HttpPost(AmbassadorRoutes.Create)]
        public async Task<ActionResult<AmbassadorDto>> Create([FromBody] AmbassadorDto dto)
        {
            var created = await _ambassadorService.AddAmbassadorAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Data.Id }, created);
        }

        /// <summary>
        /// Updates an existing ambassador by ID.
        /// </summary>
        /// <param name="id">The ID of the ambassador to update.</param>
        /// <param name="dto">The updated ambassador data.</param>
        [HttpPut(AmbassadorRoutes.Update)]
        public async Task<ActionResult<AmbassadorDto>> Update(string id, [FromBody] AmbassadorDto dto)
        {
            var updated = await _ambassadorService.EditAmbassadorAsync(id, dto);
            if (updated == null)
                return NotFound($"No ambassador found with ID {id} to update.");

            return Ok(updated);
        }

        /// <summary>
        /// Deletes an ambassador by ID.
        /// </summary>
        /// <param name="id">The ID of the ambassador to delete.</param>
        [HttpDelete(AmbassadorRoutes.Delete)]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await _ambassadorService.DeleteAmbassadorAsync(id);
            if (!deleted.Succeeded)
                return NotFound($"No ambassador found with ID {id} to delete.");

            return NoContent();
        }
    }
}
