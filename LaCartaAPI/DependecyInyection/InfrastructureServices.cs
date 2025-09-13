using Business.Modules.Dishes.Services;
using Data;
using Data.Repositories;
using Domain.Modules.Address.Interfaces;
using Domain.Modules.Dishs.Interfaces;
using Domain.Modules.Restaurants.Interfaces;
using Domain.Modules.Users.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace LaCartaAPI.DependecyInyection;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services,IConfiguration configuration)
    {
        // Base de datos
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                 b => b.MigrationsAssembly("LaCartaAPI"))
            );

        // Repositorios
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMunicipalityRepository, MunicipalityRepository>();
        services.AddScoped<IDishRepository, DishRepository>();

        return services;
    }
}