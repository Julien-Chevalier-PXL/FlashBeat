namespace FlashBeat.ExternalServices.Interfaces;

using FlashBeat.ExternalServices.Dtos.Responses;

/// <summary>
/// Interface which defines the methods of a music provider.
/// </summary>
public interface IMusicProvider
{
    /// <summary>
    /// Method to get a track by its id.
    /// </summary>
    /// <param name="trackId">The id of the track.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation containing a track.</returns>
    Task<TrackDto?> GetTrackAsync(int trackId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Method to search.
    /// </summary>
    /// <param name="query">The query to search for.</param>
    /// <param name="index">The index to start the search from. Default is 0.</param>
    /// <param name="limit">The maximum number of results to return. Default is 10.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation containing the search results.</returns>
    Task<SearchResultDto> SearchAsync(string query, int index = 0, int limit = 10, CancellationToken cancellationToken = default);
}
