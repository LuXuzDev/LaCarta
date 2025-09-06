using Business.Modules.Restaurants.DTOs;

namespace Business.Modules.Restaurants.Services;

public interface IRestaurantServices
{
    Task<IEnumerable<RestaurantDTO>> GetAllRestaurantsAsync(CancellationToken ct);

    Task<RestaurantDTO> GetByIdRestaurantAsync(int restaurantId , CancellationToken ct);
}
