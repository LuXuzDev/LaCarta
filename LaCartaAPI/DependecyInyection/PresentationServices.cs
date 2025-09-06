using FastEndpoints;
using FastEndpoints.Swagger;

namespace LaCartaAPI.DependecyInyection;

public static class PresentationServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddFastEndpoints();
        services.AddSwaggerDocument(s =>
        {
            s.Title = "LaCarta API";
            s.Description = "API para gestión de restaurantes";
            s.Version = "v1";
        });
        services.AddSwaggerGen();
        return services;
    }
}
