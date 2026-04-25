namespace FlashBeat.ExternalServices.Dtos.Responses;

/// <summary>
/// Record which represents the base DTO of a track.
/// </summary>
public record TrackBaseDto
{
    /// <summary>
    /// Gets the id of the track.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Gets the title of the track.
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    /// Gets the artist of the track.
    /// </summary>
    public required ArtistDto Artist { get; init; }
}
