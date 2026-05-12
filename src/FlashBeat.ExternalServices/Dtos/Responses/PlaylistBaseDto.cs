namespace FlashBeat.ExternalServices.Dtos.Responses;

/// <summary>
/// Record which represents the base DTO of a playlist.
/// </summary>
public record PlaylistBaseDto
{
    /// <summary>
    /// Gets the id of the playlist.
    /// </summary>
    public required long Id { get; init; }

    /// <summary>
    /// Gets the title of the playlist.
    /// </summary>
    public required string Title { get; init; }
}
