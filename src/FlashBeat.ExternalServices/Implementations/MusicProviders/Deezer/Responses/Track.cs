namespace FlashBeat.ExternalServices.Implementations.MusicProviders.Deezer.Responses;

using System.Text.Json.Serialization;

/// <summary>
/// Class which represents a track response from the Deezer API.
/// </summary>
internal sealed class Track
{
    /// <summary>
    /// Gets or sets the id of the track.
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the track.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the artist of the track.
    /// </summary>
    [JsonPropertyName("artist")]
    public Artist Artist { get; set; } = default!;

    /// <summary>
    /// Gets or sets the URI of the preview of the track.
    /// </summary>
    /// <remarks>
    /// The URI is a link to a 30 seconds preview audio file of the track.
    /// </remarks>
    [JsonPropertyName("preview")]
    public Uri Preview { get; set; } = default!;
}
