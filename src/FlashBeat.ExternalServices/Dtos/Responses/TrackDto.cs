namespace FlashBeat.ExternalServices.Dtos.Responses;

/// <summary>
/// Record which represents the DTO of a track.
/// </summary>
public sealed record TrackDto : TrackBaseDto
{
    /// <summary>
    /// Gets the audio data of the track.
    /// </summary>
    public required byte[] Audio { get; init; }
}
