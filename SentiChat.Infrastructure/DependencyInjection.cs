using Microsoft.Extensions.DependencyInjection;
using SentiChat.Application.Interfaces.Security;
using SentiChat.Domain.Interfaces.Repositories;
using SentiChat.Infrastructure.Repositories;
using SentiChat.Infrastructure.Security;

namespace SentiChat.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
