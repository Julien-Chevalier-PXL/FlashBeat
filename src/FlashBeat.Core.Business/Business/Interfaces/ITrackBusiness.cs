namespace FlashBeat.Core.Business.Business.Interfaces;

using FlashBeat.Common.Pagination;
using FlashBeat.Core.Business.Business.Models;
using FlashBeat.Core.Business.Business.ViewModels;
using FlashBeat.Core.Business.Dtos;

/// <summary>
/// Interface which defines business methods for the tracks.
/// </summary>
public interface ITrackBusiness
{
    /// <summary>
    /// Methods to get a random track.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The business result containing the track.</returns>
    Task<BusinessResult<TrackViewModel>> GetRandomTrackAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Method to get a random track from a given selection.
    /// </summary>
    /// <param name="query">The selection query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The business result containing the track.</returns>
    Task<BusinessResult<TrackViewModel>> GetRandomTrackFromSelectionAsync(SelectionQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Method to search.
    /// </summary>
    /// <param name="query">The query describing what and how to search.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A business result containing a page result of tracks corresponding to the query.</returns>
    Task<BusinessResult<PageResult<TrackBaseViewModel>>> SearchAsync(SearchQuery query, CancellationToken cancellationToken = default);
}
