namespace FlashBeat.Maui.Components.Shared.ViewModels;

using FlashBeat.Common.Enums;
using FlashBeat.Maui.Components.Shared.Enums;

/// <summary>
/// Record which represents the view model of the result of a quiz.
/// </summary>
public sealed record QuizResultViewModel
{
    /// <summary>
    /// Gets the final status of the quiz.
    /// </summary>
    public required QuizStatus Status { get; init; }

    /// <summary>
    /// Gets the last guess result.
    /// </summary>
    public required GuessResult LastGuessResult { get; init; }

    /// <summary>
    /// Gets the extract length at which the answer was found, if the quiz was successful. Null otherwise.
    /// </summary>
    public required MusicalExtractLength? FoundAtLength { get; init; }

    /// <summary>
    /// Gets the track which is the answer of the quiz.
    /// </summary>
    public required TrackBaseViewModel Track { get; init; }
}
