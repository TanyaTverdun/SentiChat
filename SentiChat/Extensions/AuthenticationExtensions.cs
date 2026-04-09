using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SentiChat.Application.Constants;
using SentiChat.Infrastructure.Configuration;
using System.Text;

namespace SentiChat.Extensions;

/// <summary>
/// Provides extension methods for configuring authentication services.
/// </summary>
public static class AuthenticationExtensions
{
    /// <summary>
    /// Configures JWT Bearer authentication for the 
    /// API using settings from the configuration.
    /// </summary>
    /// <param name="services">
    /// The IServiceCollection to add services to.
    /// </param>
    /// <param name="configuration">
    /// The application configuration containing JWT settings.
    /// </param>
    /// <returns>
    /// The modified IServiceCollection for chaining.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the 'JwtSettings:SecretKey' is missing 
    /// or empty in the configuration.
    /// </exception>
    public static IServiceCollection AddApiAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtOptions = configuration
                .GetSection(JwtOptions.SectionName)
                .Get<JwtOptions>();

        if (jwtOptions == null || string.IsNullOrEmpty(jwtOptions.SecretKey))
        {
            throw new InvalidOperationException(
                "JWT Settings are missing or invalid in configuration.");
        }

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context
                            .Request
                            .Query[SignalRConstants.AccessTokenQueryParam];
                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken) 
                                && path.StartsWithSegments(
                                        SignalRConstants.HubBasePath))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }
}
