namespace FlashBeat.Core.Business.Business;

using FlashBeat.Common.Pagination;
using FlashBeat.Core.Business.Business.Interfaces;
using FlashBeat.Core.Business.Business.Models;
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
    public async Task<BusinessResult<TrackViewModel>> GetRandomTrackAsync(CancellationToken cancellationToken = default)
    {
        var randomId = 13789091; // Orelsan - Elle viendra quand même: hardcoded for now, should be random in the future

        var result = await this.musicProvider.GetTrackAsync(randomId, cancellationToken).ConfigureAwait(false);
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
            Audio = result.Audio,
        });
    }

    /// <inheritdoc />
    public async Task<BusinessResult<PageResult<TrackBaseViewModel>>> SearchAsync(SearchQuery query, CancellationToken cancellationToken = default)
    {
        var result = await this.musicProvider.SearchAsync(query.Terms, query.StartIndex, query.PageSize, cancellationToken).ConfigureAwait(false);

        return BusinessResult<PageResult<TrackBaseViewModel>>.Success(
            new()
            {
                TotalCount = result.Total,
                Items = [.. result.Tracks.Select(t => new TrackBaseViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Artist = new ArtistViewModel
                    {
                        Id = t.Artist.Id,
                        Name = t.Artist.Name
                    },
                })],
            });
    }
}
