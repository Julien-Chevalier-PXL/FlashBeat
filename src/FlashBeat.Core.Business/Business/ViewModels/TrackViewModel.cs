namespace FlashBeat.Core.Business.Business.ViewModels;

using FlashBeat.Common.Enums;

/// <summary>
/// Record which represents the view model of a track.
/// </summary>
public sealed record TrackViewModel : TrackBaseViewModel
{
    /// <summary>
    /// Gets the audio extracts of the track, where the key is the length of the extract and the value is the audio data
    /// of the extract.
    /// </summary>
    public required Dictionary<MusicalExtractLength, byte[]> Extracts { get; init; } = [];
}
