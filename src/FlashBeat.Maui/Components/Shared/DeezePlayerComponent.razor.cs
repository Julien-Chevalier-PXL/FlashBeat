namespace FlashBeat.Maui.Components.Shared;

using Microsoft.AspNetCore.Components;

/// <summary>
/// Code-behind of the <see cref="DeezePlayerComponent"/> component.
/// </summary>
public sealed partial class DeezePlayerComponent
{
    [Parameter]
    [EditorRequired]
    public int TrackId { get; set; }

    /// <summary>
    /// Gets or sets the size (height and width), in px, of the component.
    /// </summary>
    /// <remarks>Default is 250.</remarks>
    [Parameter]
    public int Size { get; set; } = 250;
}
