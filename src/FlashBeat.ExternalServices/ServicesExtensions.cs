namespace FlashBeat.ExternalServices;

using FlashBeat.Common.Configurations;
using FlashBeat.Common.Enums;
using FlashBeat.Common.Extensions;
using FlashBeat.ExternalServices.Implementations.MusicProviders.Deezer;
using FlashBeat.ExternalServices.Interfaces;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Class providing extensions methods to register dependencies and configure services.
/// </summary>
public static class ServicesExtensions
{
    /// <summary>
    /// Adds external services to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>
    /// The same instance of <see cref="IServiceCollection"/> that was provided, to support method chaining.
    /// </returns>
    public static IServiceCollection AddExternalServicesServices(this IServiceCollection services)
    {
        services.AddMusicProvider();

        return services;
    }

    private static IServiceCollection AddMusicProvider(this IServiceCollection services)
    {
        _ = services.TryBindOptions<MusicProviderOptions>();
        services.TryAddTransient<IMusicProvider>(resolver =>
        {
            var options = resolver.GetRequiredService<MusicProviderOptions>();
            return options.Provider switch
            {
                MusicProvider.Deezer => new DeezerClient(options),
                _ => throw new NotSupportedException($"The music provider {options.Provider} is not supported."),
            };
        });

        return services;
    }
}
