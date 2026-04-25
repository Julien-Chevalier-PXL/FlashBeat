namespace FlashBeat.Maui.Components.Pages.ViewModels;

/// <summary>
/// Record which represents the view model of a track.
/// </summary>
public sealed record TrackViewModel
{
    /// <summary>
    /// Gets the id of the track.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets the title of the track.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the artist of the track.
    /// </summary>
    public ArtistViewModel Artist { get; init; } = new();

    /// <summary>
    /// Gets the audio data stream of the track.
    /// </summary>
    public Stream Audio { get; init; } = Stream.Null;
}
