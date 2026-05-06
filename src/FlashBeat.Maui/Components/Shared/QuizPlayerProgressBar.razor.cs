namespace FlashBeat.Maui.Components.Shared;

using FlashBeat.Common.Enums;

using Microsoft.AspNetCore.Components;

/// <summary>
/// Code-behind of the <see cref="QuizPlayerProgressBar" /> component.
/// </summary>
public sealed partial class QuizPlayerProgressBar
{
    private double animationDelayMs;  // only updated on Play/Resume — never on Pause
    private double pausedAtMs;         // stores position on Pause for future Resume
    private bool isPlaying;
    private bool animationToggle;
    private bool shouldReset;

    /// <summary>
    /// Gets or sets the musical extract length the progress bar should animate to.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public MusicalExtractLength ExtractLength { get; set; }

    private string AnimationClass => this.animationToggle ? "anim-a" : "anim-b";

    private static readonly Dictionary<MusicalExtractLength, double> SegmentWidths = new()
    {
        { MusicalExtractLength.PointOneSeconds,  2  },
        { MusicalExtractLength.PointFiveSeconds, 3  },
        { MusicalExtractLength.TwoSeconds,       8  },
        { MusicalExtractLength.FourSeconds,      15 },
        { MusicalExtractLength.EightSeconds,     26 },
        { MusicalExtractLength.FifteenSeconds,   46 },
    };

    private double TargetWidthPct
    {
        get
        {
            var total = 0.0;
            foreach (var (length, width) in SegmentWidths)
            {
                total += width;
                if (length == this.ExtractLength)
                    return total;
            }

            return 100.0;
        }
    }

    // The keyframe goes 0→100% but max-width caps it at TargetWidthPct.
    // Scale duration so the fill reaches max-width exactly when the audio ends.
    private int ScaledDurationMs => (int)((int)this.ExtractLength / (this.TargetWidthPct / 100.0));

    private IEnumerable<(double StartPct, double WidthPct)> Segments
    {
        get
        {
            var left = 0.0;
            foreach (var (_, width) in SegmentWidths)
            {
                yield return (left, width);
                left += width;
            }
        }
    }

    /// <summary>
    /// Starts the progress bar animation from the beginning.
    /// </summary>
    public async Task PlayAsync()
    {
        if(this.shouldReset)
        {
            this.animationDelayMs = 0;
            this.pausedAtMs = 0;
            this.isPlaying = true;
            this.animationToggle = !this.animationToggle;
            this.shouldReset = false;
        }
        else
        {
            await this.ResumeAsync().ConfigureAwait(true);
        }
        await this.InvokeAsync(this.StateHasChanged).ConfigureAwait(true);
    }

    /// <summary>
    /// Pauses the progress bar animation at its current position.
    /// </summary>
    /// <param name="currentTimeMs">The current playback time in milliseconds.</param>
    public async Task PauseAsync(double currentTimeMs)
    {
        this.pausedAtMs = currentTimeMs;  // saved for Resume — does NOT touch animationDelayMs
        this.isPlaying = false;
        await this.InvokeAsync(this.StateHasChanged).ConfigureAwait(true);
    }

    /// <summary>
    /// Resumes the progress bar animation from where it was paused.
    /// </summary>
    public async Task ResumeAsync()
    {
        this.animationDelayMs = this.pausedAtMs;  // now safe to update delay
        this.isPlaying = true;
        await this.InvokeAsync(this.StateHasChanged).ConfigureAwait(true);
    }

    /// <summary>
    /// Resets the progress bar to its initial state.
    /// </summary>
    public async Task ResetAsync()
    {
        this.animationDelayMs = 0;
        this.pausedAtMs = 0;
        this.isPlaying = false;
        this.animationToggle = !this.animationToggle;
        this.shouldReset = false;
        await this.InvokeAsync(this.StateHasChanged).ConfigureAwait(true);
    }

    public async Task PlayEndedAsync()
    {
        this.isPlaying = false;
        this.shouldReset = true;
        await this.InvokeAsync(this.StateHasChanged).ConfigureAwait(true);
    }
}
