namespace FlashBeat.Web.Service.Services.Tracks;

using FlashBeat.Common.Pagination;
using FlashBeat.Core.Business.Business.Interfaces;
using FlashBeat.Web.Service.Dtos;
using FlashBeat.Web.Service.Services.Interfaces;
using FlashBeat.Web.Service.Services.Tracks.Queries;
using FlashBeat.Web.Service.Services.Tracks.Responses;

using BusinessSelectionQueryItem = FlashBeat.Core.Business.Business.Models.SelectionQueryItem;

/// <summary>
/// Implementation of the <see cref="ITrackService"/> interface.
/// </summary>
internal sealed class TrackService : ITrackService
{
    private readonly ITrackBusiness trackBusiness;

    /// <summary>
    /// Initializes a new instance of the <see cref="TrackService"/> class.
    /// </summary>
    /// <param name="trackBusiness">The track business.</param>
    public TrackService(ITrackBusiness trackBusiness)
    {
        this.trackBusiness = trackBusiness;
    }

    /// <inheritdoc />
    public async Task<ServiceResult<TrackView>> GetRandomTrackAsync(CancellationToken cancellationToken = default)
    {
        var result = await this.trackBusiness.GetRandomTrackAsync(cancellationToken).ConfigureAwait(false);

        return new()
        {
            IsSuccess = result.IsSuccess,
            Result = TrackView.FromViewModel(result.Result),
            ErrorMessage = result.ErrorMessage,
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResult<TrackView>> GetRandomTrackFromSelectionAsync(SelectionQuery query, CancellationToken cancellationToken = default)
    {
        var result = await this.trackBusiness.GetRandomTrackFromSelectionAsync(new() { Items = [.. query.Items.Select(i => new BusinessSelectionQueryItem { Id = i.Id, Type = i.Type })] }, cancellationToken).ConfigureAwait(false);

        return new()
        {
            IsSuccess = result.IsSuccess,
            Result = TrackView.FromViewModel(result.Result),
            ErrorMessage = result.ErrorMessage,
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResult<PageResult<TrackBaseView>>> SearchAsync(SearchQuery query, CancellationToken cancellationToken = default)
    {
        var result = await this.trackBusiness.SearchAsync(new() { Terms = query.Terms, IsPaginated = query.IsPaginated, StartIndex = query.StartIndex, PageSize = query.PageSize }, cancellationToken).ConfigureAwait(false);

        return new()
        {
            IsSuccess = result.IsSuccess,
            Result = result.Result?.SelectItems(t => TrackBaseView.FromViewModel(t)) ?? new(),
            ErrorMessage = result.ErrorMessage,
        };
    }
}
