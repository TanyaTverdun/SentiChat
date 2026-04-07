using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SentiChat.Application.Interfaces;
using SentiChat.Application.Services;
using SentiChat.Application.Validators;

namespace SentiChat.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        services.AddValidatorsFromAssemblyContaining<RegisterUserDtoValidator>();

        return services;
    }
}
