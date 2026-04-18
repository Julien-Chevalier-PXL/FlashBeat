namespace FlashBeat.Common.Configurations;

/// <summary>
/// Class which represents the application settings.
/// </summary>
public sealed class ApplicationSettings : IApplicationOptions
{
    /// <inheritdoc />
    public static string SectionName => string.Empty;

    /// <summary>
    /// Gets or sets the settings of the music provider.
    /// </summary>
    public MusicProviderOptions MusicProviders { get; set; } = new();
}
