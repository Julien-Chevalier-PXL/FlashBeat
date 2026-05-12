namespace FlashBeat.ExternalServices.Implementations.MusicProviders.Deezer.Responses;

using System.Text.Json.Serialization;

/// <summary>
/// Record which represents a count result of the Deezer API.
/// </summary>
internal sealed class CountResult
{
    /// <summary>
    /// Gets or sets the total number of data.
    /// </summary>
    [JsonPropertyName("total")]
    public required int Total { get; set; }
}
