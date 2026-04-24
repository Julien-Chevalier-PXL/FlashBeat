namespace FlashBeat.Maui.Components.Pages;

using Microsoft.AspNetCore.Components;

/// <summary>
/// Code-behind of the <see cref="Home"/> component.
/// </summary>
public sealed partial class Home
{
    private readonly NavigationManager _navigationManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="Home"/> class.
    /// </summary>
    /// <param name="navigationManager">The navigation manager.</param>
    public Home(NavigationManager navigationManager)
    {
        this._navigationManager = navigationManager;
    }

    private void NavigateToInfiniteMode()
    {
        this._navigationManager.NavigateTo("/infinite");
    }
}
