namespace FlashBeat.ExternalServices.Dtos.Responses;

/// <summary>
/// Record which represents the base DTO of an album.
/// </summary>
public record AlbumBaseDto
{
    /// <summary>
    /// Gets the id of the album.
    /// </summary>
    public required long Id { get; init; }

    /// <summary>
    /// Gets the name of the album.
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    /// Gets the artist of the album.
    /// </summary>
    public required ArtistDto Artist { get; init; }
}
