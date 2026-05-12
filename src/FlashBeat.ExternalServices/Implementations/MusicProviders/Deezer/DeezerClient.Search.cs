namespace FlashBeat.ExternalServices.Implementations.MusicProviders.Deezer;

using System.Net.Http.Json;

using FlashBeat.ExternalServices.Dtos.Responses;
using FlashBeat.ExternalServices.Implementations.MusicProviders.Deezer.Responses;
using FlashBeat.ExternalServices.Interfaces;

/// <summary>
/// Client for the Deezer API.
/// </summary>
internal sealed partial class DeezerClient : IMusicProvider
{
    /// <inheritdoc />
    public async Task<SearchResultDto<TrackBaseDto>> SearchTrackAsync(string query, int index = 0, int limit = 25, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query))
            return SearchResultDto<TrackBaseDto>.Empty;

        if (limit > 100)
            throw new ArgumentOutOfRangeException(nameof(limit), "The limit must be less than or equal to 100.");

        var response = await this.httpClient.GetAsync($"search/track?q={query}&limit={limit}&index={index}", cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var errorResponseContent = await response.Content.ReadFromJsonAsync<Error>(cancellationToken).ConfigureAwait(false);
            throw new Exception($"Error while contacting the Deezer API: {errorResponseContent?.Message}");
        }

        var responseContent = await response.Content.ReadFromJsonAsync<CommonPaginatedResult<Track>>(cancellationToken).ConfigureAwait(false);
        if (responseContent is null)
            return SearchResultDto<TrackBaseDto>.Empty;

        return new()
        {
            Total = responseContent.Total,
            Data = [.. responseContent.Data.Select(t => new TrackBaseDto
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
    public async Task<SearchResultDto<ArtistDto>> SearchArtistAsync(string query, int index = 0, int limit = 10, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query))
            return SearchResultDto<ArtistDto>.Empty;

        if (limit > 100)
            throw new ArgumentOutOfRangeException(nameof(limit), "The limit must be less than or equal to 100.");

        var response = await this.httpClient.GetAsync($"search/artist?q={query}&limit={limit}&index={index}", cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var errorResponseContent = await response.Content.ReadFromJsonAsync<Error>(cancellationToken).ConfigureAwait(false);
            throw new Exception($"Error while contacting the Deezer API: {errorResponseContent?.Message}");
        }

        var responseContent = await response.Content.ReadFromJsonAsync<CommonPaginatedResult<Artist>>(cancellationToken).ConfigureAwait(false);
        if (responseContent is null)
            return SearchResultDto<ArtistDto>.Empty;

        return new()
        {
            Total = responseContent.Total,
            Data = [.. responseContent.Data.Select(t => new ArtistDto
            {
                Id = t.Id,
                Name = t.Name,
            })],
        };
    }

    /// <inheritdoc />
    public async Task<SearchResultDto<AlbumBaseDto>> SearchAlbumAsync(string query, int index = 0, int limit = 10, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query))
            return SearchResultDto<AlbumBaseDto>.Empty;

        if (limit > 100)
            throw new ArgumentOutOfRangeException(nameof(limit), "The limit must be less than or equal to 100.");

        var response = await this.httpClient.GetAsync($"search/album?q={query}&limit={limit}&index={index}", cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var errorResponseContent = await response.Content.ReadFromJsonAsync<Error>(cancellationToken).ConfigureAwait(false);
            throw new Exception($"Error while contacting the Deezer API: {errorResponseContent?.Message}");
        }

        var responseContent = await response.Content.ReadFromJsonAsync<CommonPaginatedResult<Album>>(cancellationToken).ConfigureAwait(false);
        if (responseContent is null)
            return SearchResultDto<AlbumBaseDto>.Empty;

        return new()
        {
            Total = responseContent.Total,
            Data = [.. responseContent.Data.Select(t => new AlbumBaseDto
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
    public async Task<SearchResultDto<PlaylistBaseDto>> SearchPlaylistAsync(string query, int index = 0, int limit = 10, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query))
            return SearchResultDto<PlaylistBaseDto>.Empty;

        if (limit > 100)
            throw new ArgumentOutOfRangeException(nameof(limit), "The limit must be less than or equal to 100.");

        var response = await this.httpClient.GetAsync($"search/album?q={query}&limit={limit}&index={index}", cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var errorResponseContent = await response.Content.ReadFromJsonAsync<Error>(cancellationToken).ConfigureAwait(false);
            throw new Exception($"Error while contacting the Deezer API: {errorResponseContent?.Message}");
        }

        var responseContent = await response.Content.ReadFromJsonAsync<CommonPaginatedResult<Playlist>>(cancellationToken).ConfigureAwait(false);
        if (responseContent is null)
            return SearchResultDto<PlaylistBaseDto>.Empty;

        return new()
        {
            Total = responseContent.Total,
            Data = [.. responseContent.Data.Select(t => new PlaylistBaseDto
            {
                Id = t.Id,
                Title = t.Title,
            })],
        };
    }
}
