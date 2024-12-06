using System.Text.Json.Serialization;

namespace NeuralTech.ResultWrappers
{
    /// <summary>
    /// Represents a paginated result of an operation, including success status, messages, and pagination details.
    /// </summary>
    /// <typeparam name="T">The type of the data payload.</typeparam>
    public class PaginatedResult<T> : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedResult{T}"/> class with the specified parameters.
        /// </summary>
        /// <param name="succeeded">Indicates whether the operation succeeded.</param>
        /// <param name="data">The data payload.</param>
        /// <param name="messages">The messages associated with the result.</param>
        /// <param name="count">The total count of items.</param>
        /// <param name="page">The current page number.</param>
        /// <param name="pageSize">The size of each page.</param>
        internal PaginatedResult(bool succeeded, List<T>? data = default, List<string>? messages = null, int count = 0, int page = 1, int pageSize = 10)
        {
            if (page <= 0) throw new ArgumentException("Page number must be greater than zero.", nameof(page));
            if (pageSize <= 0) throw new ArgumentException("Page size must be greater than zero.", nameof(pageSize));

            Data = data ?? new List<T>();
            CurrentPage = page;
            Succeeded = succeeded;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Messages = messages ?? new List<string>();
        }

        /// <summary>
        /// Gets or sets the data payload of the result.
        /// </summary>
        [JsonPropertyName("data")]
        public List<T> Data { get; set; }

        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the total number of pages.
        /// </summary>
        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the total count of items.
        /// </summary>
        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the size of each page.
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        /// <summary>
        /// Gets a value indicating whether there is a previous page.
        /// </summary>
        [JsonPropertyName("hasPreviousPage")]
        public bool HasPreviousPage => CurrentPage > 1;

        /// <summary>
        /// Gets a value indicating whether there is a next page.
        /// </summary>
        [JsonPropertyName("hasNextPage")]
        public bool HasNextPage => CurrentPage < TotalPages;

        /// <summary>
        /// Creates a failed paginated result with the specified messages.
        /// </summary>
        /// <param name="messages">The list of failure messages.</param>
        /// <param name="page">The current page number.</param>
        /// <param name="pageSize">The size of each page.</param>
        /// <returns>A <see cref="PaginatedResult{T}"/> indicating failure.</returns>
        public static PaginatedResult<T> Failure(List<string> messages, int page = 1, int pageSize = 10)
        {
            return new PaginatedResult<T>(false, default, messages, 0, page, pageSize);
        }

        /// <summary>
        /// Creates a successful paginated result with the specified data, count, page, and page size.
        /// </summary>
        /// <param name="data">The data payload.</param>
        /// <param name="count">The total count of items.</param>
        /// <param name="page">The current page number.</param>
        /// <param name="pageSize">The size of each page.</param>
        /// <returns>A <see cref="PaginatedResult{T}"/> indicating success.</returns>
        public static PaginatedResult<T> Success(List<T> data, int count, int page, int pageSize)
        {
            return new PaginatedResult<T>(true, data, null, count, page, pageSize);
        }

        /// <summary>
        /// Asynchronously creates a successful paginated result with the specified data task, count, page, and page size.
        /// </summary>
        /// <param name="dataTask">The task representing the data payload.</param>
        /// <param name="count">The total count of items.</param>
        /// <param name="page">The current page number.</param>
        /// <param name="pageSize">The size of each page.</param>
        /// <returns>A task representing the asynchronous operation, with a <see cref="PaginatedResult{T}"/> indicating success.</returns>
        public static async Task<PaginatedResult<T>> SuccessAsync(Task<List<T>> dataTask, int count, int page, int pageSize)
        {
            var data = await dataTask;
            return new PaginatedResult<T>(true, data, null, count, page, pageSize);
        }
    }
}