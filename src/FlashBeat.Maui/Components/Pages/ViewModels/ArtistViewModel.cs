namespace FlashBeat.Maui.Components.Pages.ViewModels;

/// <summary>
/// Record which represents the view model of an artist.
/// </summary>
public sealed record ArtistViewModel
{
    /// <summary>
    /// Gets the id of the artist.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets the name of the artist.
    /// </summary>
    public string Name { get; init; } = string.Empty;
}
