namespace FlashBeat.Web.Service.Services.Tracks.Responses;

using System.Diagnostics.CodeAnalysis;

using FlashBeat.Common.Enums;
using FlashBeat.Core.Business.Business.ViewModels;

/// <summary>
/// Record which represents the view of a track.
/// </summary>
public sealed record TrackView : TrackBaseView
{
    /// <summary>
    /// Gets the audio extracts of the track, where the key is the length of the extract and the value is the audio data
    /// of the extract.
    /// </summary>
    public required Dictionary<MusicalExtractLength, byte[]> Extracts { get; init; } = [];

    /// <summary>
    /// Method to create a <see cref="TrackView"/> from a <see cref="TrackViewModel"/>.
    /// </summary>
    /// <param name="viewModel">The view model to create the view from.</param>
    /// <returns>The corresponding <see cref="TrackView"/> if the view model is not null, otherwise null.</returns>
    [return: NotNullIfNotNull(nameof(viewModel))]
    internal static TrackView? FromViewModel(TrackViewModel? viewModel)
        => viewModel is not null
            ? new()
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Artist = ArtistView.FromViewModel(viewModel.Artist),
                Extracts = viewModel.Extracts,
            }
            : null;
}
