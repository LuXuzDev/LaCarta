using Domain.Modules.Restaurants.Models;
using Domain.Modules.Shared.Entities;

namespace Domain.Modules.Users.Models;

public class User : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

    public int RoleId { get; set; }
    public Role? Role { get; set; }

    public ICollection<Restaurant>? ManagedRestaurants { get; set; }

    public ICollection<FavoriteRestaurant>? Favorites { get; set; }
}
