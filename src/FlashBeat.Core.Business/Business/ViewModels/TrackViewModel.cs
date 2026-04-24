namespace FlashBeat.Core.Business.Business.ViewModels;

/// <summary>
/// Record which represents the view model of a track.
/// </summary>
public sealed record TrackViewModel
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
    public required ArtistViewModel Artist { get; init; }

    /// <summary>
    /// Gets the audio data stream of the track.
    /// </summary>
    public required Stream Audio { get; init; }
}
