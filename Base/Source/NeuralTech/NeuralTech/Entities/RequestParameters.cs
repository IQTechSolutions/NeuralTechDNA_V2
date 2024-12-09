namespace NeuralTech.Entities;

/// <summary>
/// Represents parameters used for paginated requests, 
/// including page number, page size, ordering, and search text.
/// </summary>
public class RequestParameters
{
    /// <summary>
    /// The maximum allowed size for a page. 
    /// This ensures that requests do not fetch excessively large datasets.
    /// </summary>
    private int _maxPageSize = 100;

    /// <summary>
    /// The default size for a page. 
    /// If not specified, this will determine the number of items per page.
    /// </summary>
    private int _pageSize = 12;

    /// <summary>
    /// Default constructor for initializing an instance of <see cref="RequestParameters"/> 
    /// with default values for its properties.
    /// </summary>
    public RequestParameters() { }

    /// <summary>
    /// Overloaded constructor for initializing an instance of <see cref="RequestParameters"/> 
    /// with specific values for page number, page size, and ordering criteria.
    /// </summary>
    /// <param name="pageNr">The page number to retrieve (defaults to 1 if not set).</param>
    /// <param name="pageSize">The number of items per page, capped by <see cref="_maxPageSize"/>.</param>
    /// <param name="orderBy">The criteria for ordering the items in the result set.</param>
    public RequestParameters(int pageNr, int pageSize, string? orderBy)
    {
        PageNr = pageNr;
        PageSize = pageSize; // Setter logic ensures it does not exceed _maxPageSize.
        OrderBy = orderBy;
    }

    /// <summary>
    /// Gets or sets the current page number to retrieve.
    /// Defaults to 1 if not explicitly set.
    /// </summary>
    public int PageNr { get; set; } = 1;

    /// <summary>
    /// Gets or sets the number of items per page.
    /// The value is capped at <see cref="_maxPageSize"/> to prevent overloading the system.
    /// </summary>
    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = value > _maxPageSize ? _maxPageSize : value; }
    }

    /// <summary>
    /// Gets or sets the field by which the result set should be ordered.
    /// This can be null if no specific ordering is required.
    /// </summary>
    public string? OrderBy { get; set; }

    /// <summary>
    /// Gets or sets the search text to filter the results.
    /// This can be null if no search filter is applied.
    /// </summary>
    public string? SearchText { get; set; }
}