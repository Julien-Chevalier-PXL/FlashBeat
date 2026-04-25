namespace FlashBeat.Web.Service.Services.Interfaces;

using FlashBeat.Common.Pagination;
using FlashBeat.Web.Service.Dtos;
using FlashBeat.Web.Service.Services.Tracks.Queries;
using FlashBeat.Web.Service.Services.Tracks.Responses;

/// <summary>
/// Interface which defines the contract for the track service, responsible for handling operations related to
/// tracks in the application.
/// </summary>
public interface ITrackService
{
    /// <summary>
    /// Method to retrieve a random track.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A service result containing the random track.</returns>
    Task<ServiceResult<TrackView>> GetRandomTrackAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Method to search.
    /// </summary>
    /// <param name="query">The query describing what and how to search.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A service result containing a page result of tracks corresponding to the query.</returns>
    Task<ServiceResult<PageResult<TrackBaseView>>> SearchAsync(SearchQuery query, CancellationToken cancellationToken = default);
}
