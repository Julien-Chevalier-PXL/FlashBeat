namespace FlashBeat.Core.Business.Business;

using FlashBeat.Core.Business.Business.Interfaces;
using FlashBeat.Core.Business.Business.ViewModels;
using FlashBeat.Core.Business.Dtos;
using FlashBeat.ExternalServices.Interfaces;

internal sealed class TrackBusiness : ITrackBusiness
{
    private readonly IMusicProvider musicProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="TrackBusiness"/> class.
    /// </summary>
    /// <param name="musicProvider">The music provider.</param>
    public TrackBusiness(IMusicProvider musicProvider)
    {
        this.musicProvider = musicProvider;
    }

    /// <inheritdoc />
    public async Task<BusinessResult<TrackViewModel>> GetRandomTrackAsync()
    {
        var randomId = 13789091; // Orelsan - Elle viendra quand même: hardcoded for now, should be random in the future

        var result = await this.musicProvider.GetTrackAsync(randomId).ConfigureAwait(false);
        if (result is null)
            return BusinessResult<TrackViewModel>.Error("Track not found.");

        return BusinessResult<TrackViewModel>.Success(new TrackViewModel
        {
            Id = result.Id,
            Title = result.Title,
            Artist = new ArtistViewModel
            {
                Id = result.Artist.Id,
                Name = result.Artist.Name
            },
            Audio = result.Audio
        });
    }
}
