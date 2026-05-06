namespace FlashBeat.Maui.Components.Shared.Dialogs;

using FlashBeat.Maui.Components.Shared.ViewModels;

using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

/// <summary>
/// Code-behind of the <see cref="QuizResultDialog"/> component.
/// </summary>
public sealed partial class QuizResultDialog : IDialogContentComponent<QuizResultViewModel>
{
    /// <summary>
    /// Gets or sets the results of the quiz to display.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public QuizResultViewModel Content { get; set; } = null!;

    [CascadingParameter]
    public FluentDialog Dialog { get; set; } = default!;

    private async Task NewQuizAsync()
    {
        await this.Dialog.CloseAsync(true).ConfigureAwait(true);
    }

    private async Task CloseAsync()
    {
        await this.Dialog.CancelAsync().ConfigureAwait(true);
    }
}
