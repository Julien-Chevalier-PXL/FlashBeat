namespace FlashBeat.Web.Service.Services.Tracks.Queries;

/// <summary>
/// Class which represents the selection query.
/// </summary>
public sealed class SelectionQuery
{
    /// <summary>
    /// Gets or sets the items of the selection query.
    /// </summary>
    public SelectionQueryItem[] Items { get; set; } = [];
}
