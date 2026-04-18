namespace FlashBeat.ExternalServices.Implementations.MusicProviders.Deezer.Responses;

using System.Text.Json.Serialization;

/// <summary>
/// Class which represents an error response from the Deezer API.
/// </summary>
internal sealed class Error
{
    /// <summary>
    /// Gets or sets the type of the error.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the message of the error.
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the code of the error.
    /// </summary>
    [JsonPropertyName("code")]
    public int Code { get; set; }
}
