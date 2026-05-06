namespace FlashBeat.Maui.Components.Shared;

using FlashBeat.Common.Enums;
using FlashBeat.Common.Extensions;
using FlashBeat.Maui.Components.Shared.Dialogs;
using FlashBeat.Maui.Components.Shared.Enums;
using FlashBeat.Maui.Components.Shared.ViewModels;
using FlashBeat.Web.Service.Services.Interfaces;
using FlashBeat.Web.Service.Services.Tracks.Queries;

using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

/// <summary>
/// Code-behind of the <see cref="QuizComponent"/> component.
/// </summary>
public partial class QuizComponent : IDisposable
{
    private readonly ITrackService trackService;
    private readonly IDialogService dialogService;

    /// <summary>
    /// Gets or sets the track to be guessed.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public TrackViewModel Track { get; set; } = TrackViewModel.Default;

    private CancellationTokenSource cancellationTokenSource = new();
    private QuizStatus quizStatus = QuizStatus.OnGoing;
    private MusicalExtractLength currentExtractLength = MusicalExtractLength.PointOneSeconds;
    private byte[] currentExtract = [];
    private List<GuessResultViewModel?> guesses = [];
    private TrackBaseViewModel? currentSelectedGuessTrack;
    private bool isTimeLocked = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuizComponent"/> class.
    /// </summary>
    /// <param name="trackService">The track service.</param>
    /// <param name="dialogService">The dialog service.</param>
    public QuizComponent(ITrackService trackService, IDialogService dialogService)
    {
        this.trackService = trackService;
        this.dialogService = dialogService;
    }

    protected override void OnParametersSet()
    {
        if (this.Track != TrackViewModel.Default)
        {
            this.UpdateCurrentExtract(EnumExtensions.First<MusicalExtractLength>());
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        this.cancellationTokenSource.Cancel();
        this.cancellationTokenSource.Dispose();
        GC.SuppressFinalize(this);
    }

    private async Task SearchTrack(OptionsSearchEventArgs<TrackBaseViewModel> args)
    {
        this.cancellationTokenSource.Cancel();
        this.cancellationTokenSource = new();

        if (string.IsNullOrWhiteSpace(args.Text))
        {
            args.Items = [];
            return;
        }

        var query = new SearchQuery
        {
            IsPaginated = true,
            PageSize = 10,
            StartIndex = 0,
            Terms = args.Text
        };
        var searchResult = await this.trackService.SearchAsync(query, this.cancellationTokenSource.Token);
        if (!searchResult.IsSuccess || searchResult.Result is null)
        {
            args.Items = [];
            return;
        }

        args.Items = searchResult.Result.Items.Select(t => new TrackBaseViewModel
        {
            Id = t.Id,
            Title = t.Title,
            Artist = new()
            {
                Id = t.Artist.Id,
                Name = t.Artist.Name,
            }
        });
    }

    private async Task SubmitAnswerAsync()
    {
        if (this.currentSelectedGuessTrack is null)
            return;

        var guessType = this.Track.Id == this.currentSelectedGuessTrack.Id
            ? GuessResult.RightTrack
            : this.Track.Artist.Id == this.currentSelectedGuessTrack.Artist.Id
                ? GuessResult.RightArtist
                : GuessResult.AllWrong;
        this.guesses.Add(new() { Type = guessType, Track = this.currentSelectedGuessTrack });

        if (guessType is GuessResult.RightTrack)
        {
            this.quizStatus = QuizStatus.Won;
            await this.ShowResultDialogAsync().ConfigureAwait(true);
        }
        else if (!this.isTimeLocked)
        {
            this.UpdateCurrentExtract(this.currentExtractLength.Next());
        }

        this.currentSelectedGuessTrack = null;
    }

    private async Task SkipAsync()
    {
        this.guesses.Add(null);
        if (this.currentExtractLength < EnumExtensions.Last<MusicalExtractLength>())
        {
            this.UpdateCurrentExtract(this.currentExtractLength.Next());
        }
    }

    private async Task GiveUpAsync()
    {
        this.guesses.Add(null);
        this.quizStatus = QuizStatus.Lost;
        await this.ShowResultDialogAsync().ConfigureAwait(true);
    }

    private void UpdateCurrentExtract(MusicalExtractLength musicalExtractLength)
    {
        this.currentExtractLength = musicalExtractLength;
        this.currentExtract = this.Track.Extracts[this.currentExtractLength];
    }

    private async Task ShowResultDialogAsync()
    {
        if (this.quizStatus is QuizStatus.OnGoing)
            return;

        var dialogParameters = new DialogParameters<QuizResultDialog>
        {
            Modal = true,
            TrapFocus = true,
            Width = "50%",
        };
        var quizResult = new QuizResultViewModel
        {
            Status = this.quizStatus,
            LastGuessResult = this.guesses.LastOrDefault()?.Type ?? GuessResult.AllWrong,
            FoundAtLength = this.currentExtractLength,
            Track = this.Track,
        };

        var dialog = await this.dialogService.ShowDialogAsync<QuizResultDialog>(quizResult, dialogParameters).ConfigureAwait(true);
        var dialogResult = await dialog.Result.ConfigureAwait(true);
        if(dialogResult.Data is bool)
        {
            // TODO: Send event to parent to start new quiz
        }
    }

    private void ToggleTimeLock()
    {
        this.isTimeLocked = !this.isTimeLocked;
    }

    private MessageIntent GetMessageIntent(GuessResult guessType)
        => guessType switch
        {
            GuessResult.RightTrack => MessageIntent.Success,
            GuessResult.RightArtist => MessageIntent.Warning,
            GuessResult.AllWrong => MessageIntent.Error,
            _ => MessageIntent.Custom,
        };
}
