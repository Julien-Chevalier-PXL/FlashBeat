namespace FlashBeat.Core.Business.Business.Models;

using FlashBeat.Common.Pagination;

/// <summary>
/// Class which represents the query to search.
/// </summary>
public sealed class SearchQuery : PageableQuery
{
    /// <summary>
    /// Gets or sets the terms to search.
    /// </summary>
    public string Terms { get; set; } = string.Empty;
}
