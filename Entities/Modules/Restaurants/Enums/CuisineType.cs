using Domain.Modules.Restaurants.Models;
using Domain.Modules.Shared.Entities;

namespace Domain.Modules.Restaurants.Enums;

public class CuisineType : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Restaurant> Restaurants = new List<Restaurant>();
}
