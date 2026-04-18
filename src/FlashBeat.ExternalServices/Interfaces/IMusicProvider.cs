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
}
