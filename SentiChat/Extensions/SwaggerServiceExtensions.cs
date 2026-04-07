using Microsoft.OpenApi.Models;
using System.Reflection;

namespace SentiChat.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(
            this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SentiChat Api",
                    Version = "v1",
                    Description = "A chat API with automatic sentiment analysis of messages via Azure AI Language.",
                    Contact = new OpenApiContact
                    {
                        Name = "Tanya Tverdun",
                        Email = "tanyatverdun@gmail.com"
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
