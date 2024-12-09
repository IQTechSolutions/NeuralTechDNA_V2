using Beneficiary.Shared.DataTransferObjects;
using NeuralTech.Entities;
using NeuralTech.Interfaces;
using NeuralTech.ResultWrappers;

// For IBaseResult and related interfaces

namespace Beneficiary.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for operations related to the Ambassador entity.
    /// Provides methods for retrieving (paged and unpaged), single entity retrieval,
    /// creating, updating, and deleting ambassadors.
    /// 
    /// Return Types:
    /// - For paged retrieval: <see cref="IBaseResult{T}"/>
    /// - For listing without paging: <see cref="IBaseResult{List{AmbassadorDto}}"/>
    /// - For single entity retrieval: <see cref="IBaseResult"/>
    /// - For add/update/delete: <see cref="IBaseResult{T}"/> or <see cref="PaginatedResult{T}"/> as appropriate.
    /// </summary>
    public interface IAmbassadorService
    {
        /// <summary>
        /// Retrieves a paged list of AmbassadorDtos according to the provided RequestParameters.
        /// Filters and sorting are applied as per SearchText and OrderBy.
        /// Paging is handled internally by the returned PaginatedResult.
        /// </summary>
        /// <param name="parameters">Defines paging (PageNr, PageSize), optional SearchText, and OrderBy fields.</param>
        /// <returns>
        /// A <see cref="PaginatedResult{AmbassadorDto}"/> representing the requested page of ambassadors.
        /// On success, contains a page of AmbassadorDto items.
        /// On failure, contains error messages.
        /// </returns>
        Task<PaginatedResult<AmbassadorDto>> GetPagedAmbassadorsAsync(RequestParameters parameters);

        /// <summary>
        /// Retrieves all AmbassadorDtos without paging.
        /// Returns a result indicating success or failure.
        /// On success, contains a list of all AmbassadorDtos.
        /// On failure, contains error messages.
        /// </summary>
        /// <returns>
        /// An <see cref="IBaseResult{List{AmbassadorDto}}"/> indicating success or failure.
        /// </returns>
        Task<IBaseResult<List<AmbassadorDto>>> GetAllAmbassadorsAsync();

        /// <summary>
        /// Retrieves a single AmbassadorDto by its unique ID.
        /// On success, returns the AmbassadorDto.
        /// On failure (e.g., not found), returns error messages.
        /// </summary>
        /// <param name="id">The unique identifier of the ambassador.</param>
        /// <returns>
        /// An <see cref="IBaseResult{AmbassadorDto}"/> indicating success or failure.
        /// </returns>
        Task<IBaseResult<AmbassadorDto>> GetAmbassadorByIdAsync(string id);

        /// <summary>
        /// Adds a new ambassador using the provided AmbassadorDto data.
        /// On success, returns the created AmbassadorDto with assigned ID.
        /// On failure, returns error messages.
        /// </summary>
        /// <param name="dto">The AmbassadorDto containing the ambassador's details.</param>
        /// <returns>
        /// An <see cref="IBaseResult{AmbassadorDto}"/> indicating success or failure.
        /// </returns>
        Task<IBaseResult<AmbassadorDto>> AddAmbassadorAsync(AmbassadorDto dto);

        /// <summary>
        /// Updates an existing ambassador identified by the given ID using the provided AmbassadorDto data.
        /// On success, returns the updated AmbassadorDto.
        /// On failure (e.g., not found, saving error), returns error messages.
        /// </summary>
        /// <param name="id">The ID of the ambassador to update.</param>
        /// <param name="dto">The updated AmbassadorDto data.</param>
        /// <returns>
        /// An <see cref="IBaseResult{AmbassadorDto}"/> indicating success or failure.
        /// </returns>
        Task<IBaseResult<AmbassadorDto>> EditAmbassadorAsync(string id, AmbassadorDto dto);

        /// <summary>
        /// Deletes an ambassador identified by the given ID.
        /// On success, returns a successful result.
        /// On failure (e.g., not found), returns error messages.
        /// </summary>
        /// <param name="id">The ID of the ambassador to delete.</param>
        /// <returns>
        /// An <see cref="IBaseResult"/> indicating success or failure.
        /// </returns>
        Task<IBaseResult> DeleteAmbassadorAsync(string id);
    }
}
