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
internal sealed partial class DeezerClient : IMusicProvider
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
    public async Task<TrackDto?> GetTrackAsync(long trackId, CancellationToken cancellationToken = default)
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
    public async Task<TrackDto?> GetTopChartTrackAsync(long genreId = 0, int index = 0, CancellationToken cancellationToken = default)
    {
        if (index > 99)
            throw new ArgumentOutOfRangeException(nameof(index), "The index must be less than or equal to 99.");

        var response = await this.httpClient.GetAsync($"chart/{genreId}/tracks?limit=1&index={index}", cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var errorResponseContent = await response.Content.ReadFromJsonAsync<Error>(cancellationToken).ConfigureAwait(false);
            throw new Exception($"Error while contacting the Deezer API: {errorResponseContent?.Message}");
        }

        var responseContent = await response.Content.ReadFromJsonAsync<CommonPaginatedResult<Track>>(cancellationToken).ConfigureAwait(false);
        if (responseContent is null)
            return null;

        var track = responseContent.Data.FirstOrDefault();
        if (track is null)
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
    public async Task<TrackDto?> GetTopTrackOfArtistAsync(long artistId, int index = 0, CancellationToken cancellationToken = default)
    {
        var response = await this.httpClient.GetAsync($"artist/{artistId}/top?limit=1&index={index}", cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var errorResponseContent = await response.Content.ReadFromJsonAsync<Error>(cancellationToken).ConfigureAwait(false);
            throw new Exception($"Error while contacting the Deezer API: {errorResponseContent?.Message}");
        }

        var responseContent = await response.Content.ReadFromJsonAsync<CommonPaginatedResult<Track>>(cancellationToken).ConfigureAwait(false);
        if (responseContent is null)
            return null;

        var track = responseContent.Data.FirstOrDefault();
        if (track is null)
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
    public async Task<TrackDto?> GetTrackOfAlbumAsync(long albumId, int index = 0, CancellationToken cancellationToken = default)
    {
        var response = await this.httpClient.GetAsync($"album/{albumId}/track?limit=1&index={index}", cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var errorResponseContent = await response.Content.ReadFromJsonAsync<Error>(cancellationToken).ConfigureAwait(false);
            throw new Exception($"Error while contacting the Deezer API: {errorResponseContent?.Message}");
        }

        var responseContent = await response.Content.ReadFromJsonAsync<CommonPaginatedResult<Track>>(cancellationToken).ConfigureAwait(false);
        if (responseContent is null)
            return null;

        var track = responseContent.Data.FirstOrDefault();
        if (track is null)
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
    public async Task<TrackDto?> GetTrackOfPlaylistAsync(long playlistId, int index = 0, CancellationToken cancellationToken = default)
    {
        var response = await this.httpClient.GetAsync($"playlist/{playlistId}/track?limit=1&index={index}", cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var errorResponseContent = await response.Content.ReadFromJsonAsync<Error>(cancellationToken).ConfigureAwait(false);
            throw new Exception($"Error while contacting the Deezer API: {errorResponseContent?.Message}");
        }

        var responseContent = await response.Content.ReadFromJsonAsync<CommonPaginatedResult<Track>>(cancellationToken).ConfigureAwait(false);
        if (responseContent is null)
            return null;

        var track = responseContent.Data.FirstOrDefault();
        if (track is null)
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
    public async Task<AlbumDto?> GetAlbumAsync(long albumId, CancellationToken cancellationToken = default)
    {
        var response = await this.httpClient.GetAsync($"album/{albumId}", cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var errorResponseContent = await response.Content.ReadFromJsonAsync<Error>(cancellationToken).ConfigureAwait(false);
            throw new Exception($"Error while contacting the Deezer API: {errorResponseContent?.Message}");
        }

        var responseContent = await response.Content.ReadFromJsonAsync<Album>(cancellationToken).ConfigureAwait(false);
        if (responseContent is null)
            return null;

        return new()
        {
            Id = responseContent.Id,
            Title = responseContent.Title,
            Artist = new()
            {
                Id = responseContent.Artist.Id,
                Name = responseContent.Artist.Name,
            },
            Tracks = [.. responseContent.Tracks.Data.Select(t => new TrackBaseDto
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

    /// <inheritdoc />
    public async Task<PlaylistDto?> GetPlaylistAsync(long playlistId, CancellationToken cancellationToken = default)
    {
        var response = await this.httpClient.GetAsync($"playlist/{playlistId}", cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var errorResponseContent = await response.Content.ReadFromJsonAsync<Error>(cancellationToken).ConfigureAwait(false);
            throw new Exception($"Error while contacting the Deezer API: {errorResponseContent?.Message}");
        }

        var responseContent = await response.Content.ReadFromJsonAsync<Playlist>(cancellationToken).ConfigureAwait(false);
        if (responseContent is null)
            return null;

        return new()
        {
            Id = responseContent.Id,
            Title = responseContent.Title,
            Tracks = [.. responseContent.Tracks.Data.Select(t => new TrackBaseDto
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

    /// <inheritdoc />
    public async Task<GenreDto[]> GetAllGenresAsync(CancellationToken cancellationToken = default)
    {
        var response = await this.httpClient.GetAsync($"genre", cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var errorResponseContent = await response.Content.ReadFromJsonAsync<Error>(cancellationToken).ConfigureAwait(false);
            throw new Exception($"Error while contacting the Deezer API: {errorResponseContent?.Message}");
        }

        var responseContent = await response.Content.ReadFromJsonAsync<CommonResult<Genre>>(cancellationToken).ConfigureAwait(false);
        if (responseContent is null)
            return [];

        return [.. responseContent.Data.Select(g => new GenreDto
        {
            Id = g.Id,
            Name = g.Name,
        })];
    }

    private static async Task<byte[]> GetTrackPreviewData(Uri previewUri, CancellationToken cancellationToken = default)
    {
        using var previewHttpClient = new HttpClient();
        var previewResponse = await previewHttpClient.GetAsync(previewUri, cancellationToken).ConfigureAwait(false);
        return await previewResponse.Content.ReadAsByteArrayAsync(cancellationToken).ConfigureAwait(false);
    }
}
