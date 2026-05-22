namespace FlashBeat.Core.Business.Business.Interfaces;

using FlashBeat.Common.Pagination;
using FlashBeat.Core.Business.Business.Models;
using FlashBeat.Core.Business.Business.ViewModels;
using FlashBeat.Core.Business.Dtos;

/// <summary>
/// Interface which defines business methods for searching.
/// </summary>
public interface ISearchBusiness
{
    /// <summary>
    /// Method to search for artists.
    /// </summary>
    /// <param name="query">The query describing what and how to search.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A business result containing a page result of artists corresponding to the query.</returns>
    Task<BusinessResult<PageResult<ArtistViewModel>>> SearchArtists(SearchQuery query, CancellationToken cancellationToken = default);
}
