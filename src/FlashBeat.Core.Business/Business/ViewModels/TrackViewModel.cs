namespace FlashBeat.Core.Business.Business.ViewModels;

/// <summary>
/// Record which represents the view model of a track.
/// </summary>
public sealed record TrackViewModel : TrackBaseViewModel
{
    /// <summary>
    /// Gets the audio data stream of the track.
    /// </summary>
    public required Stream Audio { get; init; }
}
