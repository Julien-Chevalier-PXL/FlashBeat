namespace FlashBeat.Web.Service.Services.Tracks.Responses;

using System.Diagnostics.CodeAnalysis;

using FlashBeat.Core.Business.Business.ViewModels;

/// <summary>
/// Record which represents the view of an artist.
/// </summary>
public sealed record ArtistView
{
    /// <summary>
    /// Gets the id of the artist.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Gets the name of the artist.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Method to create a <see cref="ArtistView"/> from a <see cref="ArtistViewModel"/>.
    /// </summary>
    /// <param name="viewModel">The view model to create the view from.</param>
    /// <returns>The corresponding <see cref="ArtistView"/> if the view model is not null, otherwise null.</returns>
    [return: NotNullIfNotNull(nameof(viewModel))]
    internal static ArtistView? FromViewModel(ArtistViewModel? viewModel) 
        => viewModel is not null
        ? new()
        { 
            Id = viewModel.Id,
            Name = viewModel.Name,
        }
        : null;
}
