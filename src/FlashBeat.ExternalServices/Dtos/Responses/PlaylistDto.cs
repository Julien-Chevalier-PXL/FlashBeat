namespace FlashBeat.ExternalServices.Dtos.Responses;

/// <summary>
/// Record which represents the DTO of a playlist.
/// </summary>
public sealed record PlaylistDto : PlaylistBaseDto
{
    /// <summary>
    /// Gets the tracks of the playlist.
    /// </summary>
    public required TrackBaseDto[] Tracks { get; init; }
}
