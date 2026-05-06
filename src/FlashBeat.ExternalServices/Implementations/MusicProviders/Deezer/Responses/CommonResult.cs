namespace FlashBeat.ExternalServices.Implementations.MusicProviders.Deezer.Responses;

using System.Text.Json.Serialization;

/// <summary>
/// Record which represents the common result of the Deezer API.
/// </summary>
internal sealed class CommonResult<TData>
{
    /// <summary>
    /// Gets or sets the list of data.
    /// </summary>
    [JsonPropertyName("data")]
    public required List<TData> Data { get; set; }

    /// <summary>
    /// Gets or sets the total number of data.
    /// </summary>
    [JsonPropertyName("total")]
    public required int Total { get; set; }
}
