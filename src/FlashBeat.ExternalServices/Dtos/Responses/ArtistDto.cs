namespace FlashBeat.ExternalServices.Dtos.Responses;

/// <summary>
/// Record which represents the DTO of an artists.
/// </summary>
public sealed record ArtistDto
{
    /// <summary>
    /// Gets the id of the artist.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Gets the name of the artist.
    /// </summary>
    public required string Name { get; init; }
}
