namespace FlashBeat.Core.Business.Business.Models;

using FlashBeat.Common.Enums;

/// <summary>
/// Class which represents an item of the selection query.
/// </summary>
public sealed class SelectionQueryItem
{
    /// <summary>
    /// Gets or sets the type of the item.
    /// </summary>
    public SelectionType Type { get; set; }

    /// <summary>
    /// Gets or sets the id of the item.
    /// </summary>
    public long Id { get; set; }
}
