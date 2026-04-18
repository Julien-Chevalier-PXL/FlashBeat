namespace FlashBeat.Common.Configurations;

/// <summary>
/// Interface which defines an application settings class.
/// </summary>
public interface IApplicationOptions
{
    /// <summary>
    ///     Gets the configuration section name.
    /// </summary>
    public static abstract string SectionName { get; }
}
