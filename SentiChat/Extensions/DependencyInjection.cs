using SentiChat.Application.Interfaces.Security;
using SentiChat.Security;

namespace SentiChat.Extensions;

/// <summary>
/// Provides extension methods for registering web-specific services in the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers web-related services, such as HTTP context access and current user resolution.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <returns>The modified IServiceCollection for chaining.</returns>
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
