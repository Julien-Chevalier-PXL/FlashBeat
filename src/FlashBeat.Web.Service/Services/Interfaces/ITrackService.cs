namespace FlashBeat.Web.Service.Services.Interfaces;

using FlashBeat.Web.Service.Dtos;
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
    /// <returns>A service result containing the random track.</returns>
    Task<ServiceResult<TrackView>> GetRandomTrackAsync();
}
