namespace FlashBeat.Web.Service.Services.Tracks.Responses;

using System.Diagnostics.CodeAnalysis;

using FlashBeat.Core.Business.Business.ViewModels;

/// <summary>
/// Record which represents the base view of a track.
/// </summary>
public record TrackBaseView
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
    /// Method to create a <see cref="TrackBaseView"/> from a <see cref="TrackBaseViewModel"/>.
    /// </summary>
    /// <param name="viewModel">The view model to create the view from.</param>
    /// <returns>The corresponding <see cref="TrackBaseView"/> if the view model is not null, otherwise null.</returns>
    [return: NotNullIfNotNull(nameof(viewModel))]
    internal static TrackBaseView? FromViewModel(TrackBaseViewModel? viewModel)
        => viewModel is not null
            ? new()
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Artist = ArtistView.FromViewModel(viewModel.Artist),
            }
            : null;
}
