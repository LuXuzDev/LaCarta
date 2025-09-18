using Domain.Modules.Address;
using Domain.Modules.Dishs.Models;
using Domain.Modules.Restaurants.Enums;
using Domain.Modules.Shared.Entities;
using Domain.Modules.Users.Models;

namespace Domain.Modules.Restaurants.Models;

public class Restaurant : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    //public string? QrCode { get; set;}
    public string? Image { get; set; }
    public bool HasDelivery { get; set; }
    public string? Description { get; set; }
    public TimeSpan OpenHour { get; set; }
    public TimeSpan CloseHour { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public int MunicipalityId { get; set; }
    public Municipality? Municipality { get; set; }

    public CuisineType? CuisineType { get; set; }

    public ICollection<RestaurantTag> RestaurantTags { get; set; }

    public ICollection<Dish> Dishes { get; set; } = new List<Dish>();
}
