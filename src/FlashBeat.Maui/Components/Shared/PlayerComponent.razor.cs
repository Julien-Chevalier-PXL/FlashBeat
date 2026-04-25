namespace FlashBeat.Maui.Components.Shared;

using FlashBeat.Maui.Components.Shared.Enums;

using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;

using Icons = Microsoft.FluentUI.AspNetCore.Components.Icons;

/// <summary>
/// Code-behind of the <see cref="PlayerComponent"/> component.
/// </summary>
public sealed partial class PlayerComponent : IAsyncDisposable
{
    private readonly IJSRuntime jsRuntime;

    private IJSObjectReference? jsModule;

    private bool isAudioPlaying = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerComponent"/> class.
    /// </summary>
    /// <param name="jsRuntime">The JS runtime.</param>
    public PlayerComponent(IJSRuntime jsRuntime)
    {
        this.jsRuntime = jsRuntime;
    }

    [Parameter]
    [EditorRequired]
    public Stream Audio { get; set; } = Stream.Null;

    /// <summary>
    /// Gets or sets the size of the player component.
    /// </summary>
    /// <remarks>Default is <see cref="PlayerComponentSize.Medium"/>.</remarks>
    [Parameter]
    public PlayerComponentSize Size { get; set; } = PlayerComponentSize.Medium;

    private Icon PauseIcon
        => this.Size switch
        {
            PlayerComponentSize.Small => new Icons.Regular.Size24.Pause(),
            PlayerComponentSize.Medium => new Icons.Regular.Size32.Pause(),
            PlayerComponentSize.Large => new Icons.Regular.Size48.Pause(),
            _ => new Icons.Regular.Size32.Pause(),
        };

    private Icon PlayIcon
        => this.Size switch
        {
            PlayerComponentSize.Small => new Icons.Regular.Size24.Play(),
            PlayerComponentSize.Medium => new Icons.Regular.Size32.Play(),
            PlayerComponentSize.Large => new Icons.Regular.Size48.Play(),
            _ => new Icons.Regular.Size32.Play(),
        };

    protected override async Task OnInitializedAsync()
    {
        this.jsModule = await this.jsRuntime.InvokeAsync<IJSObjectReference>("import", $"./Components/Shared/{nameof(PlayerComponent)}.razor.js");
    }

    protected override async Task OnParametersSetAsync()
    {
        if (this.jsModule is not null)
        {
            using var contentStreamReference = new DotNetStreamReference(this.Audio);
            await this.jsModule.InvokeVoidAsync("SetupAudioFileStream", contentStreamReference);
        }
    }

    private async Task PlayOrPauseAsync()
    {
        if (this.jsModule is null)
            return;

        try
        {
            if (this.isAudioPlaying)
                await this.jsModule.InvokeVoidAsync("PauseAudioFileStream");
            else
                await this.jsModule.InvokeVoidAsync("ResumeAudioFileStream");

            this.isAudioPlaying = !this.isAudioPlaying;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during play/pause: {ex.Message}");
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (this.jsModule is null)
            return;

        await this.jsModule.InvokeVoidAsync("CleanupAudio");
        await this.jsModule.DisposeAsync();
    }

    private string GetCssClass()
        => this.Size switch
        {
            PlayerComponentSize.Small => "small",
            PlayerComponentSize.Medium => "medium",
            PlayerComponentSize.Large => "large",
            _ => "medium",
        };
}
