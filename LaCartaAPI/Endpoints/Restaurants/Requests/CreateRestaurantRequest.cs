using Domain.Modules.Restaurants.Enums;

namespace LaCartaAPI.Endpoints.Restaurants.Requests;

public class CreateRestaurantRequest
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public IFormFile Image { get; set; }
    public bool HasDelivery { get; set; }

    public TimeSpan OpenHour { get; set; }
    public TimeSpan CloseHour { get; set; }

    public int UserId { get; set; }
    public int MunicipalityId { get; set; }

    public CuisineType CuisineType { get; set; }
    public ICollection<RestaurantTag> RestaurantTags { get; set; }
}