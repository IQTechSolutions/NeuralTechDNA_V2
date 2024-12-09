using Beneficiary.Entities;
using Beneficiary.Services.Interfaces;
using Beneficiary.Shared.DataTransferObjects;
using NeuralTech.Entities;
using NeuralTech.EntityFramework.Interfaces;
using NeuralTech.Interfaces;
using NeuralTech.ResultWrappers;

// for IRepository<Ambassador, string>
// for Result, Result<T>, PaginatedResult<T>, etc.

// for IBaseResult, IBaseResult<T>

namespace Beneficiary.Services.Implimentation
{
    /// <summary>
    /// Provides service-level logic for the Ambassador entity, including:
    /// - Retrieving ambassadors (paged and unpaged)
    /// - Retrieving a single ambassador by ID (via condition, no GetByIdAsync)
    /// - Adding, editing, and deleting ambassadors
    /// 
    /// Returns results as Result, Result<T>, or PaginatedResult<T>, all implementing IBaseResult or IBaseResult<T>.
    /// </summary>
    public class AmbassadorService : IAmbassadorService
    {
        private readonly IRepository<Ambassador, string> _repository;

        /// <summary>
        /// Initializes a new instance of the AmbassadorService with a specified repository for data access.
        /// </summary>
        /// <param name="repository">The data repository for Ambassador entities.</param>
        public AmbassadorService(IRepository<Ambassador, string> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves a paged list of AmbassadorDtos.
        /// According to the instructions:
        /// - Must return PaginatedResult<AmbassadorDto> directly.
        /// - PaginatedResult handles paging internally; do not manually apply skip/take.
        /// - Filter and sort in memory, then pass the entire dataset to PaginatedResult.
        /// </summary>
        /// <param name="parameters">Request parameters (page, pageSize, searchText, orderBy).</param>
        /// <returns>A PaginatedResult<AmbassadorDto> representing the requested page of data.</returns>
        public async Task<PaginatedResult<AmbassadorDto>> GetPagedAmbassadorsAsync(RequestParameters parameters)
        {
            // Retrieve data depending on SearchText
            var result = string.IsNullOrWhiteSpace(parameters.SearchText)
                ? await _repository.FindAllAsync(trackChanges: false)
                : await _repository.FindByConditionAsync(
                    a => a.Name.Contains(parameters.SearchText) || a.Surname.Contains(parameters.SearchText),
                    trackChanges: false
                  );

            if (!result.Succeeded || result.Data == null)
            {
                // Return failure paginated result
                return PaginatedResult<AmbassadorDto>.Failure(result.Messages, parameters.PageNr, parameters.PageSize);
            }

            var ambassadors = result.Data;

            // Apply sorting in memory
            if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
            {
                if (parameters.OrderBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    ambassadors = ambassadors.OrderBy(a => a.Name).ToList();
                else if (parameters.OrderBy.Equals("Surname", StringComparison.OrdinalIgnoreCase))
                    ambassadors = ambassadors.OrderBy(a => a.Surname).ToList();
            }

            var totalCount = ambassadors.Count;

            // Map to DTO
            var dtoList = ambassadors.Select(a => new AmbassadorDto
            {
                Id = a.Id,
                Name = a.Name,
                Surname = a.Surname,
                PhoneNr = a.PhoneNr,
                Email = a.Email,
                CommissionPercentage = a.CommissionPercentage
            }).ToList();

            // Return a PaginatedResult<AmbassadorDto> success without manually paging in this method.
            return PaginatedResult<AmbassadorDto>.Success(dtoList, totalCount, parameters.PageNr, parameters.PageSize);
        }

        /// <summary>
        /// Retrieves all ambassadors as DTOs.
        /// Returns a Result<List<AmbassadorDto>> indicating success or failure.
        /// </summary>
        public async Task<IBaseResult<List<AmbassadorDto>>> GetAllAmbassadorsAsync()
        {
            var allResult = await _repository.FindAllAsync(trackChanges: false);
            if (!allResult.Succeeded || allResult.Data == null)
                return await Result<List<AmbassadorDto>>.FailAsync(allResult.Messages);

            var dtoList = allResult.Data.Select(a => new AmbassadorDto
            {
                Id = a.Id,
                Name = a.Name,
                Surname = a.Surname,
                PhoneNr = a.PhoneNr,
                Email = a.Email,
                CommissionPercentage = a.CommissionPercentage
            }).ToList();

            return Result<List<AmbassadorDto>>.Success(dtoList);
        }

        /// <summary>
        /// Retrieves a single ambassador by ID without using GetByIdAsync.
        /// Uses FindByConditionAsync(a => a.Id == id) to fetch the ambassador.
        /// Returns a Result<AmbassadorDto> indicating success or failure.
        /// </summary>
        /// <param name="id">The unique ID of the ambassador.</param>
        public async Task<IBaseResult<AmbassadorDto>> GetAmbassadorByIdAsync(string id)
        {
            // Find by condition since we cannot use GetByIdAsync
            var findResult = await _repository.FindByConditionAsync(a => a.Id == id, trackChanges: false);
            if (!findResult.Succeeded || findResult.Data == null || findResult.Data.Count == 0)
                return await Result<AmbassadorDto>.FailAsync($"No ambassador found with ID {id}.");

            // Take the first entity found
            var ambassador = findResult.Data.First();

            var dto = new AmbassadorDto
            {
                Id = ambassador.Id,
                Name = ambassador.Name,
                Surname = ambassador.Surname,
                PhoneNr = ambassador.PhoneNr,
                Email = ambassador.Email,
                CommissionPercentage = ambassador.CommissionPercentage
            };

            return await Result<AmbassadorDto>.SuccessAsync(dto);
        }

        /// <summary>
        /// Adds a new ambassador from an AmbassadorDto and returns the created DTO.
        /// Returns a Result<AmbassadorDto> indicating success or failure.
        /// </summary>
        /// <param name="dto">The DTO representing the new ambassador's data.</param>
        public async Task<IBaseResult<AmbassadorDto>> AddAmbassadorAsync(AmbassadorDto dto)
        {
            var ambassador = new Ambassador
            {
                Id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                Surname = dto.Surname,
                PhoneNr = dto.PhoneNr,
                Email = dto.Email,
                CommissionPercentage = dto.CommissionPercentage
            };

            var createResult = await _repository.CreateAsync(ambassador);
            if (!createResult.Succeeded || createResult.Data == null)
                return await Result<AmbassadorDto>.FailAsync(createResult.Messages);

            var saveResult = await _repository.SaveAsync();
            if (!saveResult.Succeeded)
                return await Result<AmbassadorDto>.FailAsync(saveResult.Messages);

            dto.Id = ambassador.Id;
            return await Result<AmbassadorDto>.SuccessAsync(dto);
        }

        /// <summary>
        /// Updates an existing ambassador by ID using the given AmbassadorDto data.
        /// Uses FindByConditionAsync to locate the entity.
        /// Returns a Result<AmbassadorDto> indicating success or failure.
        /// </summary>
        /// <param name="id">The ID of the ambassador to update.</param>
        /// <param name="dto">The updated ambassador data.</param>
        public async Task<IBaseResult<AmbassadorDto>> EditAmbassadorAsync(string id, AmbassadorDto dto)
        {
            // Find the ambassador by condition
            var findResult = await _repository.FindByConditionAsync(a => a.Id == id, trackChanges: false);
            if (!findResult.Succeeded || findResult.Data == null || findResult.Data.Count == 0)
                return Result<AmbassadorDto>.Fail($"No ambassador found with ID {id} to update.");

            var ambassador = findResult.Data.First();
            ambassador.Name = dto.Name;
            ambassador.Surname = dto.Surname;
            ambassador.PhoneNr = dto.PhoneNr;
            ambassador.Email = dto.Email;
            ambassador.CommissionPercentage = dto.CommissionPercentage;

            var updateResult = await _repository.UpdateAsync(ambassador);
            if (!updateResult.Succeeded || updateResult.Data == null)
                return await Result<AmbassadorDto>.FailAsync(updateResult.Messages);

            var saveResult = await _repository.SaveAsync();
            if (!saveResult.Succeeded)
                return await Result<AmbassadorDto>.FailAsync(saveResult.Messages);

            return await Result<AmbassadorDto>.SuccessAsync(dto);
        }

        /// <summary>
        /// Deletes an ambassador by ID without using GetByIdAsync.
        /// Uses FindByConditionAsync(a => a.Id == id) to locate the entity.
        /// Returns a Result indicating success or failure.
        /// </summary>
        /// <param name="id">The ID of the ambassador to delete.</param>
        public async Task<IBaseResult> DeleteAmbassadorAsync(string id)
        {
            var findResult = await _repository.FindByConditionAsync(a => a.Id == id, trackChanges: false);
            if (!findResult.Succeeded || findResult.Data == null || findResult.Data.Count == 0)
                return Result.Fail($"No ambassador found with ID {id} to delete.");

            var ambassador = findResult.Data.First();

            var deleteResult = await _repository.DeleteAsync(ambassador);
            if (!deleteResult.Succeeded)
                return await Result.FailAsync(deleteResult.Messages);

            var saveResult = await _repository.SaveAsync();
            if (!saveResult.Succeeded)
                return await Result.FailAsync(saveResult.Messages);

            return Result.Success();
        }
    }
}
