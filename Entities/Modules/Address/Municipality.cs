using Domain.Modules.Restaurants.Models;
using Domain.Modules.Shared.Entities;
using Domain.Modules.Users.Models;

namespace Domain.Modules.Address;

public class Municipality : BaseEntity
{
    public required string Name { get; set; }

    //Relacion User 1 -- n Municipality
    public ICollection<User> Users = new List<User>();

    //Relacion Restaurant 1 -- n Municipality
    public ICollection<Restaurant> Restaurants = new List<Restaurant>();
}
