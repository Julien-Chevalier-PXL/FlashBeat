namespace FlashBeat.ExternalServices.Dtos.Responses;

/// <summary>
/// Record which represents the DTO of a genre.
/// </summary>
public sealed record GenreDto
{
    /// <summary>
    /// Gets the id of the genre.
    /// </summary>
    public required long Id { get; init; }

    /// <summary>
    /// Gets the name of the genre.
    /// </summary>
    public required string Name { get; init; }
}
