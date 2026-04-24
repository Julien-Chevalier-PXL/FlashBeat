namespace FlashBeat.Core.Business.Business.Interfaces;

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
    /// <returns>The business result containing the track.</returns>
    Task<BusinessResult<TrackViewModel>> GetRandomTrackAsync();
}
