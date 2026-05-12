namespace FlashBeat.ExternalServices.Implementations.MusicProviders.Deezer.Responses;

using System.Text.Json.Serialization;

/// <summary>
/// Record which represents the common result of the Deezer API.
/// </summary>
internal class CommonResult<TData>
{
    /// <summary>
    /// Gets an empty common result.
    /// </summary>
    public static CommonResult<Track> Empty => new() { Data = [] };

    /// <summary>
    /// Gets or sets the list of data.
    /// </summary>
    [JsonPropertyName("data")]
    public required List<TData> Data { get; set; }
}
