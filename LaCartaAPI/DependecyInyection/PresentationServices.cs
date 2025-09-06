using Microsoft.OpenApi.Models;

namespace LaCartaAPI.DependecyInyection;

public static class PresentationServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        // Controladores
        services.AddControllers();

        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Mi API", Version = "v1" });
        });

        return services;
    }
}