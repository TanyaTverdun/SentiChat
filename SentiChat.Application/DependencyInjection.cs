using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SentiChat.Application.Interfaces;
using SentiChat.Application.Services;
using SentiChat.Application.Validators;

namespace SentiChat.Application;

/// <summary>
/// Provides extension methods to configure dependency 
/// injection for the Application layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers all application-level services, validators, 
    /// and other dependencies into the service collection.
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to add the services to.
    /// </param>
    /// <returns>
    /// The updated <see cref="IServiceCollection"/> for chaining.
    /// </returns>
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IMessageService, MessageService>();

        services
            .AddValidatorsFromAssemblyContaining<RegisterUserDtoValidator>();

        return services;
    }
}
