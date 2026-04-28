namespace FlashBeat.Maui.Components.Shared.ViewModels;

using FlashBeat.Maui.Components.Shared.Enums;

/// <summary>
/// Classe which represents the view model of a guess.
/// </summary>
internal sealed record GuessResultViewModel
{
    /// <summary>
    /// Gets the type of guess.
    /// </summary>
    public required GuessResult Type { get; init; }

    /// <summary>
    /// Gets the track of the guess.
    /// </summary>
    public required TrackBaseViewModel Track { get; init; }
}
