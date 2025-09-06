using Business.Modules.Restaurants.Mappers;
using Business.Modules.Restaurants.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LaCartaAPI.DependecyInyection;


public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //Servicios de negocio
        services.AddScoped<IRestaurantServices, RestaurantServices>();

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
        return services;
    }
}
