namespace FlashBeat.Core.Business.Business.ViewModels;

/// <summary>
/// Record which represents the view model of an artist.
/// </summary>
public sealed record ArtistViewModel
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
