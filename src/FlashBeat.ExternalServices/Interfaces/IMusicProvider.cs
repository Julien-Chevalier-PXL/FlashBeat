namespace FlashBeat.ExternalServices.Interfaces;

using FlashBeat.ExternalServices.Dtos.Responses;

/// <summary>
/// Interface which defines the methods of a music provider.
/// </summary>
public interface IMusicProvider
{
    /// <summary>
    /// Method to get a track by its id.
    /// </summary>
    /// <param name="trackId">The id of the track.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation containing the track.</returns>
    Task<TrackDto?> GetTrackAsync(long trackId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Method to get a top chart track by its index and genre id.
    /// </summary>
    /// <param name="genreId">The id of the genre. By default 0 (all genres).</param>
    /// <param name="index">The index in the chart.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation containing a top chart track.</returns>
    Task<TrackDto?> GetTopChartTrackAsync(long genreId = 0, int index = 0, CancellationToken cancellationToken = default);

    /// <summary>
    /// Method to get a top chart track by its index and genre id.
    /// </summary>
    /// <param name="index">The index in the chart.</param>
    /// <param name="artistId">The id of the artist.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation containing a top chart track.</returns>
    Task<TrackDto?> GetTopTrackOfArtistAsync(long artistId, int index = 0, CancellationToken cancellationToken = default);

    /// <summary>
    /// Method to get a track of an album.
    /// </summary>
    /// <param name="index">The index of the track in the album.</param>
    /// <param name="albumId">The id of the album.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation containing the track.</returns>
    Task<TrackDto?> GetTrackOfAlbumAsync(long albumId, int index = 0, CancellationToken cancellationToken = default);

    /// <summary>
    /// Method to get a track of a playlist.
    /// </summary>
    /// <param name="index">The index of the track in the playlist.</param>
    /// <param name="playlistId">The id of the playlist.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation containing the track.</returns>
    Task<TrackDto?> GetTrackOfPlaylistAsync(long playlistId, int index = 0, CancellationToken cancellationToken = default);

    /// <summary>
    /// Method to get an album by its id.
    /// </summary>
    /// <param name="albumId">The id of the album.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation containing the album.</returns>
    Task<AlbumDto?> GetAlbumAsync(long albumId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Method to get a playlist by its id.
    /// </summary>
    /// <param name="playlistId">The id of the playlist.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation containing the playlist.</returns>
    Task<PlaylistDto?> GetPlaylistAsync(long playlistId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Method to get all the genres.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation containing the list of genres.</returns>
    Task<GenreDto[]> GetAllGenresAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Method to search.
    /// </summary>
    /// <param name="query">The query to search for.</param>
    /// <param name="index">The index to start the search from. Default is 0.</param>
    /// <param name="limit">The maximum number of results to return. Default is 10.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation containing the search results.</returns>
    Task<SearchResultDto<TrackBaseDto>> SearchTrackAsync(string query, int index = 0, int limit = 10, CancellationToken cancellationToken = default);

    /// <summary>
    /// Method to search an artist.
    /// </summary>
    /// <param name="query">The query to search for.</param>
    /// <param name="index">The index to start the search from. Default is 0.</param>
    /// <param name="limit">The maximum number of results to return. Default is 10.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation containing the search results.</returns>
    Task<SearchResultDto<ArtistDto>> SearchArtistAsync(string query, int index = 0, int limit = 10, CancellationToken cancellationToken = default);

    /// <summary>
    /// Method to search an album.
    /// </summary>
    /// <param name="query">The query to search for.</param>
    /// <param name="index">The index to start the search from. Default is 0.</param>
    /// <param name="limit">The maximum number of results to return. Default is 10.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation containing the search results.</returns>
    Task<SearchResultDto<AlbumBaseDto>> SearchAlbumAsync(string query, int index = 0, int limit = 10, CancellationToken cancellationToken = default);

    /// <summary>
    /// Method to search a playlist.
    /// </summary>
    /// <param name="query">The query to search for.</param>
    /// <param name="index">The index to start the search from. Default is 0.</param>
    /// <param name="limit">The maximum number of results to return. Default is 10.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation containing the search results.</returns>
    Task<SearchResultDto<PlaylistBaseDto>> SearchPlaylistAsync(string query, int index = 0, int limit = 10, CancellationToken cancellationToken = default);

    /// <summary>
    /// Method to get the number of tracks in an album.
    /// </summary>
    /// <param name="albumId">The id of the album.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation containing the number of tracks.</returns>
    Task<int> GetAlbumTrackCountAsync(long albumId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Method to get the number of tracks in a playlist.
    /// </summary>
    /// <param name="playlistId">The id of the playlist.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation containing the number of tracks.</returns>
    Task<int> GetPlaylistTrackCountAsync(long playlistId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Method to get the number of tracks in the top chart of a genre.
    /// </summary>
    /// <param name="genreId">The id of the genre.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation containing the number of tracks.</returns>
    Task<int> GetGenreTopChartTrackCountAsync(long genreId, CancellationToken cancellationToken = default);
}
