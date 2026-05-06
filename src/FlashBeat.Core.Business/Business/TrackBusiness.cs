namespace FlashBeat.Core.Business.Business;

using FlashBeat.Common.Enums;
using FlashBeat.Common.Pagination;
using FlashBeat.Core.Business.Business.Interfaces;
using FlashBeat.Core.Business.Business.Models;
using FlashBeat.Core.Business.Business.ViewModels;
using FlashBeat.Core.Business.Dtos;
using FlashBeat.ExternalServices.Interfaces;

using NAudio.Wave;
using NAudio.Wave.SampleProviders;

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
        var randomId = Random.Shared.Next(100);

        var result = await this.musicProvider.GetTopChartTrackAsync(randomId, cancellationToken: cancellationToken).ConfigureAwait(false);
        if (result is null)
            return BusinessResult<TrackViewModel>.Error("Track not found.");

        var audios = GetQuizExtracts(result.Audio).ToDictionary(x => x.ExtractLength, x => x.Data);

        return BusinessResult<TrackViewModel>.Success(new TrackViewModel
        {
            Id = result.Id,
            Title = result.Title,
            Artist = new ArtistViewModel
            {
                Id = result.Artist.Id,
                Name = result.Artist.Name
            },
            Extracts = audios,
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

    private static IEnumerable<(MusicalExtractLength ExtractLength, byte[] Data)> GetQuizExtracts(byte[] trackData)
    {
        using var mp3Stream = new MemoryStream(trackData);
        using var mp3Reader = new Mp3FileReader(mp3Stream);

        foreach (var length in Enum.GetValues<MusicalExtractLength>())
        {
            mp3Reader.Position = 0;
            var sampleProvider = mp3Reader.ToSampleProvider();
            var offsetProvider = new OffsetSampleProvider(sampleProvider)
            {
                Take = TimeSpan.FromMilliseconds((int)length),
            };
            using var outMs = new MemoryStream();
            WaveFileWriter.WriteWavFileToStream(outMs, offsetProvider.ToWaveProvider());
            yield return (length, outMs.ToArray());
        }
    }
}
