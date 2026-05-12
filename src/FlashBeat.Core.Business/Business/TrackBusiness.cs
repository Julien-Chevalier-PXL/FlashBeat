namespace FlashBeat.Core.Business.Business;

using FlashBeat.Common.Enums;
using FlashBeat.Common.Pagination;
using FlashBeat.Core.Business.Business.Interfaces;
using FlashBeat.Core.Business.Business.Models;
using FlashBeat.Core.Business.Business.ViewModels;
using FlashBeat.Core.Business.Dtos;
using FlashBeat.ExternalServices.Delegates;
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

    public async Task<BusinessResult<TrackViewModel>> GetRandomTrackFromSelectionAsync(SelectionQuery query, CancellationToken cancellationToken = default)
    {
        var (selectionItem, indexInSelection) = await this.GetRandomItemFromSelection(query, cancellationToken).ConfigureAwait(false);

        GetTrackOfDelegate getTrackDelegate = selectionItem.Type switch
        {
            SelectionType.Album => this.musicProvider.GetTrackOfAlbumAsync,
            SelectionType.Artist => this.musicProvider.GetTopTrackOfArtistAsync,
            SelectionType.Playlist => this.musicProvider.GetTrackOfPlaylistAsync,
            SelectionType.GenreChart => this.musicProvider.GetTopChartTrackAsync,
            _ => throw new NotSupportedException($"Selection type '{selectionItem.Type}' is not supported."),
        };

        var track = await getTrackDelegate(selectionItem.Id, indexInSelection, cancellationToken).ConfigureAwait(false);
        if (track is null)
            return BusinessResult<TrackViewModel>.Error("Track not found.");

        var audios = GetQuizExtracts(track.Audio).ToDictionary(x => x.ExtractLength, x => x.Data);

        return BusinessResult<TrackViewModel>.Success(new TrackViewModel
        {
            Id = track.Id,
            Title = track.Title,
            Artist = new ArtistViewModel
            {
                Id = track.Artist.Id,
                Name = track.Artist.Name
            },
            Extracts = audios,
        });
    }

    /// <inheritdoc />
    public async Task<BusinessResult<PageResult<TrackBaseViewModel>>> SearchAsync(SearchQuery query, CancellationToken cancellationToken = default)
    {
        var result = await this.musicProvider.SearchTrackAsync(query.Terms, query.StartIndex, query.PageSize, cancellationToken).ConfigureAwait(false);

        return BusinessResult<PageResult<TrackBaseViewModel>>.Success(
            new()
            {
                TotalCount = result.Total,
                Items = [.. result.Data.Select(t => new TrackBaseViewModel
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

    private async Task<(SelectionQueryItem SelectionItem, int IndexInSelection)> GetRandomItemFromSelection(SelectionQuery query, CancellationToken cancellationToken)
    {
        var countTasks = query.Items.Select(async item =>
                    item.Type switch
                    {
                        SelectionType.Album => (SelectionItem: item, Count: await this.musicProvider.GetAlbumTrackCountAsync(item.Id, cancellationToken).ConfigureAwait(false)),
                        SelectionType.Artist => (SelectionItem: item, Count: 100),
                        SelectionType.Playlist => (SelectionItem: item, Count: await this.musicProvider.GetPlaylistTrackCountAsync(item.Id, cancellationToken).ConfigureAwait(false)),
                        SelectionType.GenreChart => (SelectionItem: item, Count: await this.musicProvider.GetGenreTopChartTrackCountAsync(item.Id, cancellationToken).ConfigureAwait(false)),
                        _ => throw new NotSupportedException($"Selection type '{item.Type}' is not supported."),
                    }
                );

        var counts = await Task.WhenAll(countTasks).ConfigureAwait(false);
        var totalBbTracks = counts.Sum(x => x.Count);
        var randomIndex = Random.Shared.Next(0, totalBbTracks);

        var orderedItems = counts.OrderBy(x => x.SelectionItem.Id).ThenBy(x => x.SelectionItem.Type).ToArray();

        var currentIndex = 0;
        var currentArrayIndex = 0;
        while (currentIndex < randomIndex)
        {
            currentIndex += orderedItems[currentArrayIndex].Count;
            currentArrayIndex++;
        }

        var (selectionItem, selectionItemCount) = orderedItems[currentArrayIndex];
        var indexInSelection = randomIndex - currentIndex - selectionItemCount;
        return (selectionItem, indexInSelection);
    }
}
