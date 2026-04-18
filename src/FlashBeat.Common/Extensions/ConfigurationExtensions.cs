namespace FlashBeat.Common.Extensions;

using FlashBeat.Common.Configurations;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

/// <summary>
/// Static class providing extensions methods for the application configuration.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Binds and adds the specified <typeparamref name="TApplicationOptions"/> to the service collection.
    /// </summary>
    /// <typeparam name="TApplicationOptions">The typeof the settings.</typeparam>
    /// <param name="services">The service collection.</param>
    /// <returns>The completed service collection.</returns>
    public static IServiceCollection TryBindOptions<TApplicationOptions>(this IServiceCollection services)
            where TApplicationOptions : class, IApplicationOptions
    {
        if (services.Contains(ServiceDescriptor.Singleton<TApplicationOptions, TApplicationOptions>()))
            return services;

        _ = services.AddOptions<TApplicationOptions>()
                    .BindConfiguration(TApplicationOptions.SectionName)
                    .ValidateDataAnnotations()
                    .ValidateOnStart();

        services.TryAddSingleton(resolver => resolver.GetRequiredService<IOptions<TApplicationOptions>>().Value);

        return services;
    }
}
