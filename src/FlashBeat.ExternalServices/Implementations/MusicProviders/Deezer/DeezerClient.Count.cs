namespace FlashBeat.ExternalServices.Implementations.MusicProviders.Deezer;

using System.Net.Http.Json;

using FlashBeat.ExternalServices.Implementations.MusicProviders.Deezer.Responses;
using FlashBeat.ExternalServices.Interfaces;

/// <summary>
/// Client for the Deezer API.
/// </summary>
internal sealed partial class DeezerClient : IMusicProvider
{
    /// <inheritdoc />
    public async Task<int> GetAlbumTrackCountAsync(long albumId, CancellationToken cancellationToken = default)
        => await this.GetTrackCount("album", albumId, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<int> GetPlaylistTrackCountAsync(long playlistId, CancellationToken cancellationToken = default)
        => await this.GetTrackCount("playlist", playlistId, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<int> GetGenreTopChartTrackCountAsync(long genreId, CancellationToken cancellationToken = default)
        => await this.GetTrackCount("chart", genreId, cancellationToken).ConfigureAwait(false);

    private async Task<int> GetTrackCount(string endpoint, long id, CancellationToken cancellationToken = default)
    {
        var response = await this.httpClient.GetAsync($"{endpoint}/{id}/tracks?limit=1", cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var errorResponseContent = await response.Content.ReadFromJsonAsync<Error>(cancellationToken).ConfigureAwait(false);
            throw new Exception($"Error while contacting the Deezer API: {errorResponseContent?.Message}");
        }

        var responseContent = await response.Content.ReadFromJsonAsync<CountResult>(cancellationToken).ConfigureAwait(false);
        if (responseContent is null)
            return 0;

        return responseContent.Total;
    }
}
