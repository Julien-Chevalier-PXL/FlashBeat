namespace FlashBeat.ExternalServices.Implementations.MusicProviders.Deezer.Responses;

using System.Text.Json.Serialization;

/// <summary>
/// Record which represents the result of a search query to the Deezer API.
/// </summary>
internal sealed class SearchResult
{
    /// <summary>
    /// Gets or sets the list of tracks matching the search query.
    /// </summary>
    [JsonPropertyName("data")]
    public required List<Track> Tracks { get; set; }

    /// <summary>
    /// Gets or sets the total number of tracks matching the search query.
    /// </summary>
    [JsonPropertyName("total")]
    public required int Total { get; set; }
}
