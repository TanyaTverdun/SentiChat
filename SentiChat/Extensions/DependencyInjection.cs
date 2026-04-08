using SentiChat.Application.Interfaces.Security;
using SentiChat.Security;

namespace SentiChat.Extensions;

/// <summary>
/// Provides extension methods for registering web-specific services in the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers web-related services, such as HTTP context access, current user resolution, and Real-Time communication.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <param name="configuration">The application configuration containing Azure settings.</param>
    /// <returns>The modified IServiceCollection for chaining.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the 'Azure:SignalR:ConnectionString' is missing or empty in the configuration.
    /// </exception>
    public static IServiceCollection AddWebServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        var signalRConnectionString = configuration["Azure:SignalR:ConnectionString"];
        if (string.IsNullOrEmpty(signalRConnectionString))
        {
            throw new InvalidOperationException(
                "Azure SignalR connection string is missing.");
        }

        services.AddSignalR().AddAzureSignalR(signalRConnectionString);

        return services;
    }
}
