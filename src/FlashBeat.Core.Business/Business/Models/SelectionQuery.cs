namespace FlashBeat.Core.Business.Business.Models;

/// <summary>
/// Class which represents the query to select a track from a specified selection.
/// </summary>
public sealed class SelectionQuery
{
    /// <summary>
    /// Gets or sets the items of the selection query.
    /// </summary>
    public SelectionQueryItem[] Items { get; set; } = [];
}
