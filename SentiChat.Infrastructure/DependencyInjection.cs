using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SentiChat.Application.Interfaces.AzureAI;
using SentiChat.Application.Interfaces.Security;
using SentiChat.Domain.Interfaces.Repositories;
using SentiChat.Infrastructure.Configuration;
using SentiChat.Infrastructure.Data;
using SentiChat.Infrastructure.ExternalServices.AzureAI;
using SentiChat.Infrastructure.Repositories;
using SentiChat.Infrastructure.Security;

namespace SentiChat.Infrastructure;

/// <summary>
/// Provides extension methods to configure 
/// dependency injection for the Infrastructure layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers infrastructure-level services such as database repositories, 
    /// security providers, and external API clients.
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to add the services to.
    /// </param>
    /// <param name="configuration">
    /// The application configuration to read settings from.
    /// </param>
    /// <returns>
    /// The updated <see cref="IServiceCollection"/> for chaining.
    /// </returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddSingleton<
            ISentimentAnalysisService, SentimentAnalysisService>();

        services.AddScoped<SentiChatDbInitializer>();

        return services;
    }
}
