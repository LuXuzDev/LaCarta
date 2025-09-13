using Domain.Modules.Address;
using Domain.Modules.Dishs.Models;
using Domain.Modules.Restaurants.Models;
using Domain.Modules.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Dish> Dishs { get; set; }
    public DbSet<DishWarning> DishWarnings { get; set; }
    public DbSet<Municipality> Municipality { get; set; }

    //Clase intermedia relacion n <-> m entre User y Restaurant
    public DbSet<FavoriteRestaurant> FavoriteRestaurants { get; set; }

    //Clase intermedia relacion n <-> m entre WarningType y Dish
    public DbSet<DishWarning> WarningTypeDishes { get; set; }

}
