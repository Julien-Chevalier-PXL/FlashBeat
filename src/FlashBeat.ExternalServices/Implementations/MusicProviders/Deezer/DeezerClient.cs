namespace FlashBeat.ExternalServices.Implementations.MusicProviders.Deezer;

using System.Net.Http.Json;

using FlashBeat.Common.Configurations;
using FlashBeat.Common.Enums;
using FlashBeat.ExternalServices.Dtos.Responses;
using FlashBeat.ExternalServices.Implementations.MusicProviders.Deezer.Responses;
using FlashBeat.ExternalServices.Interfaces;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Client for the Deezer API.
/// </summary>
internal sealed class DeezerClient : IMusicProvider
{
    private readonly HttpClient httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeezerClient"/> class.
    /// </summary>
    /// <param name="settings">The settings for the music provider.</param>
    public DeezerClient([FromKeyedServices(MusicProvider.Deezer)] MusicProviderOptions settings)
    {
        this.httpClient = new HttpClient
        {
            BaseAddress = new Uri(settings.ApiBaseUrl),
        };
    }

    /// <inheritdoc />
    public async Task<TrackDto?> GetTrackAsync(int trackId, CancellationToken cancellationToken = default)
    {
        var response = await this.httpClient.GetAsync($"track/{trackId}", cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var errorResponseContent = await response.Content.ReadFromJsonAsync<Error>(cancellationToken).ConfigureAwait(false);
            throw new Exception($"Error while contacting the Deezer API: {errorResponseContent?.Message}");
        }

        var responseContent = await response.Content.ReadFromJsonAsync<Track>(cancellationToken).ConfigureAwait(false);
        if (responseContent is null)
            return null;

        var previewData = await GetTrackPreviewData(responseContent.Preview, cancellationToken).ConfigureAwait(false);

        return new()
        {
            Id = responseContent.Id,
            Title = responseContent.Title,
            Audio = previewData,
            Artist = new()
            {
                Id = responseContent.Artist.Id,
                Name = responseContent.Artist.Name,
            }
        };
    }

    /// <inheritdoc />
    public async Task<TrackDto?> GetTopChartTrackAsync(int index = 0, int? genreId = null, CancellationToken cancellationToken = default)
    {
        if (index > 99)
            throw new ArgumentOutOfRangeException(nameof(index), "The index must be less than or equal to 99.");

        var response = await this.httpClient.GetAsync($"chart/{genreId ?? 0}/tracks?limit=1&index={index}", cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var errorResponseContent = await response.Content.ReadFromJsonAsync<Error>(cancellationToken).ConfigureAwait(false);
            throw new Exception($"Error while contacting the Deezer API: {errorResponseContent?.Message}");
        }

        var responseContent = await response.Content.ReadFromJsonAsync<CommonResult<Track>>(cancellationToken).ConfigureAwait(false);
        if (responseContent is null)
            return null;

        var track = responseContent.Data.FirstOrDefault();
        if(track is null)
            return null;

        var previewData = await GetTrackPreviewData(track.Preview, cancellationToken).ConfigureAwait(false);

        return new TrackDto
        {
            Id = track.Id,
            Title = track.Title,
            Audio = previewData,
            Artist = new ArtistDto
            {
                Id = track.Artist.Id,
                Name = track.Artist.Name,
            }
        };
    }

    /// <inheritdoc />
    public async Task<SearchResultDto> SearchAsync(string query, int index = 0, int limit = 25, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query))
            return SearchResultDto.Empty;

        if (limit > 100)
            throw new ArgumentOutOfRangeException(nameof(limit), "The limit must be less than or equal to 100.");

        var response = await this.httpClient.GetAsync($"search?q={query}&limit={limit}&index={index}", cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var errorResponseContent = await response.Content.ReadFromJsonAsync<Error>(cancellationToken).ConfigureAwait(false);
            throw new Exception($"Error while contacting the Deezer API: {errorResponseContent?.Message}");
        }

        var responseContent = await response.Content.ReadFromJsonAsync<CommonResult<Track>>(cancellationToken).ConfigureAwait(false);
        if (responseContent is null)
            return SearchResultDto.Empty;

        return new()
        {
            Total = responseContent.Total,
            Tracks = [.. responseContent.Data.Select(t => new TrackBaseDto
            {
                Id = t.Id,
                Title = t.Title,
                Artist = new()
                {
                    Id = t.Artist.Id,
                    Name = t.Artist.Name,
                },
            })],
        };
    }

    private static async Task<byte[]> GetTrackPreviewData(Uri previewUri, CancellationToken cancellationToken = default)
    {
        using var previewHttpClient = new HttpClient();
        var previewResponse = await previewHttpClient.GetAsync(previewUri, cancellationToken).ConfigureAwait(false);
        return await previewResponse.Content.ReadAsByteArrayAsync(cancellationToken).ConfigureAwait(false);
    }
}
