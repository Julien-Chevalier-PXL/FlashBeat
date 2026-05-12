namespace FlashBeat.ExternalServices.Implementations.MusicProviders.Deezer.Responses;

using System.Text.Json.Serialization;

/// <summary>
/// Class which represents an album response from the Deezer API.
/// </summary>
internal sealed class Album
{
    /// <summary>
    /// Gets or sets the id of the artist.
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the album.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the artist of the album.
    /// </summary>
    [JsonPropertyName("artist")]
    public Artist Artist { get; set; } = new();

    /// <summary>
    /// Gets or sets the tracks of the album.
    /// </summary>
    [JsonPropertyName("tracks")]
    public CommonResult<Track> Tracks { get; set; } = CommonResult<Track>.Empty;
}
