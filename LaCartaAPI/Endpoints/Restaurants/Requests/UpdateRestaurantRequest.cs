using Domain.Modules.Restaurants.Enums;

namespace LaCartaAPI.Endpoints.Restaurants.Requests;

public class UpdateRestaurantRequest
{
    public required int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public IFormFile? Image { get; set; }
    public bool HasDelivery { get; set; }
    public bool IsActive { get; set; }
    public TimeSpan? OpenHour { get; set; }
    public TimeSpan? CloseHour { get; set; }

    public int? MunicipalityId { get; set; }

    public CuisineType? CuisineType { get; set; }
    public ICollection<RestaurantTag> RestaurantTags { get; set; }
}
