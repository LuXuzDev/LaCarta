using FastEndpoints;


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
        });
        return services;
    }
}
