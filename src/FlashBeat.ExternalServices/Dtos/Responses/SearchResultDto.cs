namespace FlashBeat.ExternalServices.Dtos.Responses;

/// <summary>
/// Record which represents the DTO of a search result.
/// </summary>
public sealed record SearchResultDto
{
    /// <summary>
    /// Gets an empty search result.
    /// </summary>
    public static SearchResultDto Empty => new() { Total = 0, Tracks = [] };

    /// <summary>
    /// Gets or sets the list of tracks matching the search query.
    /// </summary>
    public required TrackBaseDto[] Tracks { get; set; }

    /// <summary>
    /// Gets or sets the total number of tracks matching the search query.
    /// </summary>
    public required int Total { get; set; }
}
