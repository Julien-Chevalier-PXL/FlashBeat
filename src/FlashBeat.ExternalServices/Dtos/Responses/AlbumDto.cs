namespace FlashBeat.ExternalServices.Dtos.Responses;

/// <summary>
/// Record which represents the DTO of an album.
/// </summary>
public sealed record AlbumDto : AlbumBaseDto
{
    /// <summary>
    /// Gets the tracks of the album.
    /// </summary>
    public required TrackBaseDto[] Tracks { get; init; }
}
