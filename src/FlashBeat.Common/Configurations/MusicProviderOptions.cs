namespace FlashBeat.Common.Configurations;

using System.ComponentModel.DataAnnotations;

using FlashBeat.Common.Enums;

/// <summary>
/// Class which represents the settings of a music provider.
/// </summary>
public sealed class MusicProviderOptions : IApplicationOptions
{
    /// <inheritdoc />
    public static string SectionName => "MusicProvider";

    /// <summary>
    /// Gets or sets the music provider.
    /// </summary>
    [Required]
    public MusicProvider Provider { get; set; } = MusicProvider.Undefined;

    /// <summary>
    /// Gets or sets the base URL of the API of the music provider.
    /// </summary>
    [Required]
    public string ApiBaseUrl { get; set; } = string.Empty;
}
