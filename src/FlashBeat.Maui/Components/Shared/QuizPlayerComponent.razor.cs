namespace FlashBeat.Maui.Components.Shared;

using FlashBeat.Common.Enums;

using Microsoft.AspNetCore.Components;

/// <summary>
/// Code-behind of the <see cref="QuizPlayerComponent"/> component.
/// </summary>
public sealed partial class QuizPlayerComponent
{
    private PlayerComponent? playerComponent;
    private QuizPlayerProgressBar? quizPlayerProgressBar;

    /// <summary>
    /// Gets or sets the extract length to be played.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public MusicalExtractLength ExtractLength { get; set; }

    /// <summary>
    /// Gets or sets the extract to be played.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public byte[] Extract { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the component is disabled.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Method to reset the player and the progress bar.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task ResetAsync()
    {
        if (this.playerComponent is not null)
            await this.playerComponent.StopAsync().ConfigureAwait(true);

        if (this.quizPlayerProgressBar is not null)
            await this.quizPlayerProgressBar.ResetAsync().ConfigureAwait(true);
    }

    private async Task OnPlayEventHandlerAsync()
    {
        if (this.quizPlayerProgressBar is not null)
            await this.quizPlayerProgressBar.PlayAsync().ConfigureAwait(true);
    }

    private async Task OnPauseEventHandlerAsync(double audioTime)
    {
        if (this.quizPlayerProgressBar is not null)
            await this.quizPlayerProgressBar.PauseAsync(audioTime).ConfigureAwait(true);
    }

    private async Task OnStopEventHandlerAsync()
    {
        if (this.quizPlayerProgressBar is not null)
            await this.quizPlayerProgressBar.ResetAsync().ConfigureAwait(true);
    }

    private async Task OnAudioEndedEventHandlerAsync()
    {
        if (this.quizPlayerProgressBar is not null)
            await this.quizPlayerProgressBar.PlayEndedAsync().ConfigureAwait(true);
    }
}
