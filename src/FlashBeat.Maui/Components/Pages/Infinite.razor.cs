namespace FlashBeat.Maui.Components.Pages;

using FlashBeat.Maui.Components.Pages.ViewModels;
using FlashBeat.Web.Service.Services.Interfaces;

using Microsoft.FluentUI.AspNetCore.Components;

/// <summary>
/// Code-behind of the <see cref="Infinite"/> component.
/// </summary>
public sealed partial class Infinite
{
    private readonly ITrackService trackService;
    private readonly IToastService toastService;

    private bool isLoading = false;

    private TrackViewModel currentTrack = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Infinite"/> class.
    /// </summary>
    /// <param name="trackService">The track service.</param>
    /// <param name="toastService">The toast service.</param>
    public Infinite(ITrackService trackService, IToastService toastService)
    {
        this.trackService = trackService;
        this.toastService = toastService;
    }

    protected override async Task OnInitializedAsync()
    {
        await this.LoadRandomTrackAsync().ConfigureAwait(true);
    }

    private async Task LoadRandomTrackAsync()
    {
        this.isLoading = true;

        var serviceResult = await this.trackService.GetRandomTrackAsync().ConfigureAwait(true);
        if (serviceResult.IsSuccess && serviceResult.Result is not null)
        {
            var result = serviceResult.Result;
            this.currentTrack = new()
            {
                Id = result.Id,
                Title = result.Title,
                Artist = new()
                {
                    Id = result.Artist.Id,
                    Name = result.Artist.Name,
                },
                Audio = result.Audio,
            };
        }
        else
        {
            this.toastService.ShowToast(ToastIntent.Error, $"Erreur lors de la récupération de la chanson: {serviceResult.ErrorMessage}");
        }

        this.isLoading = false;
    }
}
