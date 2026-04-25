namespace FlashBeat.Common.Pagination;

/// <summary>
/// Represents a single page of results from a paginated query.
/// </summary>
/// <typeparam name="TResult">The type of the items contained in the page.</typeparam>
public class PageResult<TResult>
{
    /// <summary>
    /// Gets or sets the collection of items of the page.
    /// </summary>
    public TResult[] Items { get; set; } = [];

    /// <summary>
    /// Gets or sets the total number of items.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Method to create a new instance of <see cref="PageResult{TSelect}"/> with the items transformed by the provided
    /// selector function.
    /// </summary>
    /// <typeparam name="TSelect">The type of the items in the resulting page.</typeparam>
    /// <param name="selector">The select function.</param>
    /// <returns>A new <see cref="PageResult{TSelect}"/> with the transformed items.</returns>
    public PageResult<TSelect> SelectItems<TSelect>(Func<TResult, TSelect> selector)
        => new() { TotalCount = this.TotalCount, Items = [.. this.Items.Select(selector)] } ;
}
