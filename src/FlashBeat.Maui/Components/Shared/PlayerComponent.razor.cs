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
    private readonly Guid elementId = Guid.NewGuid();

    private IJSObjectReference? jsModule;

    private bool audioChanged = false;
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
    public byte[] Audio { get; set; } = [];

    /// <summary>
    /// Gets or sets the size of the player component.
    /// </summary>
    /// <remarks>Default is <see cref="PlayerComponentSize.Medium"/>.</remarks>
    [Parameter]
    public PlayerComponentSize Size { get; set; } = PlayerComponentSize.Medium;

    /// <summary>
    /// Gets or sets a value indicating whether to show a stop button.
    /// </summary>
    [Parameter]
    public bool ShowStopButton { get; set; } = false;

    /// <summary>
    /// Gets or sets a callback that is invoked when the audio starts playing.
    /// </summary>
    [Parameter]
    public EventCallback OnPlay { get; set; }

    /// <summary>
    /// Gets or sets a callback that is invoked when the audio is paused.
    /// </summary>
    [Parameter]
    public EventCallback<double> OnPause { get; set; }

    /// <summary>
    /// Gets or sets a callback that is invoked when the audio is stopped.
    /// </summary>
    [Parameter]
    public EventCallback OnStop { get; set; }

    /// <summary>
    /// Gets or sets a callback that is invoked when the audio has ended.
    /// </summary>
    [Parameter]
    public EventCallback OnAudioEnded { get; set; }

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

    public override Task SetParametersAsync(ParameterView parameters)
    {
        if (parameters.TryGetValue<byte[]>(nameof(this.Audio), out var audio)
            && this.Audio != audio
            && audio.Length > 0)
        {
            this.audioChanged = true;
        }

        return base.SetParametersAsync(parameters);
    }

    protected override async Task OnParametersSetAsync()
    {
        if (this.jsModule is not null && this.Audio.Length > 0 && this.audioChanged)
        {
            await this.PauseAsync().ConfigureAwait(true);
            using var contentStreamReference = new DotNetStreamReference(new MemoryStream(this.Audio));
            await this.jsModule.InvokeVoidAsync("SetupAudioFileStream", this.elementId, contentStreamReference);
            this.audioChanged = false;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (this.jsModule is null)
            return;

        await this.jsModule.InvokeVoidAsync("CleanupAudio", this.elementId);
        await this.jsModule.DisposeAsync();
    }

    public async Task PlayOrPauseAsync()
    {
        if (this.isAudioPlaying)
            await this.PauseAsync().ConfigureAwait(true);
        else
            await this.PlayAsync().ConfigureAwait(true);
    }

    public async Task StopAsync()
    {
        if (this.jsModule is null)
            return;

        await this.jsModule.InvokeVoidAsync("StopAudioFileStream", this.elementId).ConfigureAwait(true);
        await this.OnStop.InvokeAsync().ConfigureAwait(true);

        this.isAudioPlaying = false;
    }

    public async Task PlayAsync()
    {
        if (this.isAudioPlaying || this.jsModule is null)
            return;

        try
        {
            await this.jsModule.InvokeVoidAsync("ResumeAudioFileStream", this.elementId).ConfigureAwait(true);
            await this.OnPlay.InvokeAsync().ConfigureAwait(true);

            this.isAudioPlaying = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during play: {ex.Message}");
        }
    }

    public async Task PauseAsync()
    {
        if (!this.isAudioPlaying || this.jsModule is null)
            return;

        try
        {
            var audioTime = await this.jsModule.InvokeAsync<double>("PauseAudioFileStream", this.elementId).ConfigureAwait(true);
            await this.OnPause.InvokeAsync(audioTime).ConfigureAwait(true);

            this.isAudioPlaying = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during pause: {ex.Message}");
        }
    }

    private async Task OnAudioEndedAsync()
    {
        await this.OnAudioEnded.InvokeAsync().ConfigureAwait(true);

        this.isAudioPlaying = false;
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
