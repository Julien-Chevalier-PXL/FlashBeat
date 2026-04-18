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

        using var previewHttpClient = new HttpClient();
        var previewResponse = await previewHttpClient.GetAsync(responseContent!.Preview, cancellationToken).ConfigureAwait(false);
        var previewStream = await previewResponse.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);

        return new()
        {
            Id = responseContent.Id,
            Title = responseContent.Title,
            Audio = previewStream,
            Artist = new()
            {
                Id = responseContent.Artist.Id,
                Name = responseContent.Artist.Name,
            }
        };
    }
}
