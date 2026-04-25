namespace FlashBeat.Common.Pagination;

/// <summary>
/// Class which represents a query to retrieve a page of items.
/// </summary>
public class PageableQuery
{
    public static readonly PageableQuery Default = new();
    public static readonly PageableQuery NotPaginated = new() { IsPaginated = false };

    /// <summary>
    /// Gets or sets a value indicating whether the query should retrieve a paginated result or not.
    /// </summary>
    public bool IsPaginated { get; set; } = true;

    /// <summary>
    /// Gets or sets the start index.
    /// </summary>
    public int StartIndex { get; set; } = 0;

    /// <summary>
    /// Gets or sets the number of items to include on each page of results.
    /// </summary>
    public int PageSize { get; set; } = 10;
}
