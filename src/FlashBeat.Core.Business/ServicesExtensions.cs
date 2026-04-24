namespace FlashBeat.Core.Business;

using FlashBeat.Core.Business.Business;
using FlashBeat.Core.Business.Business.Interfaces;
using FlashBeat.ExternalServices;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Class providing extensions methods to register dependencies and configure services.
/// </summary>
public static class ServicesExtensions
{
    /// <summary>
    /// Adds the businessservices to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>
    /// The same instance of <see cref="IServiceCollection"/> that was provided, to support method chaining.
    /// </returns>
    public static IServiceCollection AddBusinessServices(this IServiceCollection services) 
        => services.AddExternalServicesServices()
                   .AddBusinesses();

    private static IServiceCollection AddBusinesses(this IServiceCollection services)
    {
        services.TryAddTransient<ITrackBusiness, TrackBusiness>();

        return services;
    }
}
