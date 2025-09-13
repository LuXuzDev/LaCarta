using Business.Modules.Dishes.Mappers;
using Business.Modules.Dishes.Services;
using Business.Modules.Restaurants.Mappers;
using Business.Modules.Restaurants.Services;
using Data.FileStorage;
using Domain.FileStorage;



namespace LaCartaAPI.DependecyInyection;


public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //Servicios de negocio
        services.AddScoped<IRestaurantServices, RestaurantServices>();
        services.AddScoped<IDishServices, DishServices>();

        //Servicio Imagenes
        services.AddScoped<IFileStorageService, LocalFileStorageService>();

        //Cors
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", b =>
            {
                b.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        //Servicios de Autorizacion y Autentificacion
        services.AddAuthorization();
        services.AddAuthentication();

        //Servicios de mappeo
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(RestaurantProfile).Assembly));
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ApiRestaurantProfile).Assembly));
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ApiDishProfile).Assembly));
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(DishProfile).Assembly));
        
        return services;
    }
}
