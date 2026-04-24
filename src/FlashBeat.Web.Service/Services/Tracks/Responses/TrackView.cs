namespace FlashBeat.Web.Service.Services.Tracks.Responses;

using System.Diagnostics.CodeAnalysis;

using FlashBeat.Core.Business.Business.ViewModels;

/// <summary>
/// Record which represents the view of a track.
/// </summary>
public sealed record TrackView
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
    public required ArtistView Artist { get; init; }

    /// <summary>
    /// Gets the audio data stream of the track.
    /// </summary>
    public required Stream Audio { get; init; }

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
                Audio = viewModel.Audio,
            }
            : null;
}
