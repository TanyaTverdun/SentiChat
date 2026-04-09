using Microsoft.OpenApi.Models;
using System.Reflection;

namespace SentiChat.Extensions;

/// <summary>
/// Provides extension methods for configuring 
/// Swagger OpenAPI documentation.
/// </summary>
public static class SwaggerServiceExtensions
{
    /// <summary>
    /// Adds and configures Swagger documentation, 
    /// including XML comments and JWT authentication UI.
    /// </summary>
    /// <param name="services">
    /// The IServiceCollection to add services to.
    /// </param>
    /// <returns>
    /// The modified IServiceCollection for chaining.
    /// </returns>
    public static IServiceCollection AddSwaggerDocumentation(
        this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "SentiChat Api",
                Version = "v1",
                Description = "A chat API with automatic sentiment analysis " +
                    "of messages via Azure AI Language.",
                Contact = new OpenApiContact
                {
                    Name = "Tanya Tverdun",
                    Email = "tanyatverdun@gmail.com"
                }
            });

            var appXmlFile = "SentiChat.Application.xml";
            var appXmlPath = Path.Combine(AppContext.BaseDirectory, appXmlFile);

            if (File.Exists(appXmlPath))
            {
                c.IncludeXmlComments(appXmlPath);
            }

            var xmlFilename = $"{Assembly
                .GetExecutingAssembly()
                .GetName()
                .Name}.xml";

            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
            c.IncludeXmlComments(xmlPath);
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. " +
                    "Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });

        return services;
    }
}
