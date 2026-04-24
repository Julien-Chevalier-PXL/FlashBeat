namespace FlashBeat.Web.Service.Services.Tracks;

using FlashBeat.Core.Business.Business.Interfaces;
using FlashBeat.Web.Service.Dtos;
using FlashBeat.Web.Service.Services.Interfaces;
using FlashBeat.Web.Service.Services.Tracks.Responses;

/// <summary>
/// Implementation of the <see cref="ITrackService"/> interface.
/// </summary>
internal sealed class TrackService : ITrackService
{
    private readonly ITrackBusiness trackBusiness;

    /// <summary>
    /// Initializes a new instance of the <see cref="TrackService"/> class.
    /// </summary>
    /// <param name="trackBusiness">The track business.</param>
    public TrackService(ITrackBusiness trackBusiness)
    {
        this.trackBusiness = trackBusiness;
    }

    /// <inheritdoc />
    public async Task<ServiceResult<TrackView>> GetRandomTrackAsync()
    {
        var result = await this.trackBusiness.GetRandomTrackAsync().ConfigureAwait(false);

        return new()
        {
            IsSuccess = result.IsSuccess,
            Result = TrackView.FromViewModel(result.Result),
            ErrorMessage = result.ErrorMessage,
        };
    }
}
