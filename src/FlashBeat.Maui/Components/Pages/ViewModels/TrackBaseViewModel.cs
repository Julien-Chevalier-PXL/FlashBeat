namespace FlashBeat.Maui.Components.Pages.ViewModels;

public record TrackBaseViewModel
{
    /// <summary>
    /// Gets the id of the track.
    /// </summary>
    public long Id { get; init; }

    /// <summary>
    /// Gets the title of the track.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the artist of the track.
    /// </summary>
    public ArtistViewModel Artist { get; init; } = new();

    /// <summary>
    /// Gets the display text of the track.
    /// </summary>
    public string DisplayText => $"{this.Artist.Name} - {this.Title}";
}
