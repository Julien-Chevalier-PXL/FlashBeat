namespace FlashBeat.Maui;

using System.Reflection;

using Microsoft.Extensions.Configuration;

/// <summary>
/// Class providing extensions methods to register dependencies and configure services.
/// </summary>
public static class ServicesExtensions
{
    /// <summary>
    /// Method to add the appsettings.json configuration file to the application configuration.
    /// </summary>
    /// <param name="builder">The MAUI app builder.</param>
    /// <returns>
    /// The same instance of <see cref="MauiAppBuilder"/> that was provided, to support method chaining.
    /// </returns>
    public static MauiAppBuilder AddAppSettingsConfiguration(this MauiAppBuilder builder)
    {
        var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        using var appSettingsStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{assemblyName}.wwwroot.appsettings.json");
        if (appSettingsStream != null)
        {
            var config = new ConfigurationBuilder()
                .AddJsonStream(appSettingsStream)
                .Build();

            builder.Configuration.AddConfiguration(config);
        }

        return builder;
    }
}
