namespace FlashBeat.Maui.Components.Pages.ViewModels;

/// <summary>
/// Record which represents the view model of a track.
/// </summary>
public sealed record TrackViewModel : TrackBaseViewModel
{
    /// <summary>
    /// Gets the audio data of the track.
    /// </summary>
    public byte[] Audio { get; init; } = [];
}
