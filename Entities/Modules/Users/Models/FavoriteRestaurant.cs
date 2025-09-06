using Domain.Modules.Restaurants.Models;
using Domain.Modules.Shared.Entities;

namespace Domain.Modules.Users.Models;

public class FavoriteRestaurant : BaseEntity
{
    public int UserId { get; set; }
    public User? User { get; set; }

    public int RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
}
