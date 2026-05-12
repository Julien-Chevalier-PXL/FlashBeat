namespace FlashBeat.ExternalServices.Dtos.Responses;

/// <summary>
/// Record which represents the DTO of a search result.
/// </summary>
public sealed record SearchResultDto<TData>
{
    /// <summary>
    /// Gets an empty search result.
    /// </summary>
    public static SearchResultDto<TData> Empty => new() { Total = 0, Data = [] };

    /// <summary>
    /// Gets or sets the list of data matching the search query.
    /// </summary>
    public required TData[] Data { get; set; }

    /// <summary>
    /// Gets or sets the total number of data matching the search query.
    /// </summary>
    public required int Total { get; set; }
}
