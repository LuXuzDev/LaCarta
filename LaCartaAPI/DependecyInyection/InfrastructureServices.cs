using Data;
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

        return services;
    }
}