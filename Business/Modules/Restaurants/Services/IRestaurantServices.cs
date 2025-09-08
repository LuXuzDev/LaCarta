using Business.Modules.Restaurants.DTOs;
using Domain.Modules.Restaurants.Enums;


namespace Business.Modules.Restaurants.Services;

public interface IRestaurantServices
{
    Task<IEnumerable<RestaurantDTO>> GetRestaurantsAsync(CancellationToken ct);

    Task<RestaurantDTO> GetByIdRestaurantAsync(int restaurantId, CancellationToken ct);

    Task<IEnumerable<RestaurantDTO>> GetRestaurantsByManagerIdAsync
        (int restaurantManagerId, CancellationToken ct);

    Task<IEnumerable<RestaurantDTO>> GetRestaurantsActivesAsync(CancellationToken ct);

    Task<IEnumerable<RestaurantDTO>> GetByRestaurantsCousineTypeAsync(CuisineType cousineType, CancellationToken ct);

    Task CreateRestaurantAsync(CreateRestaurantDTO restaurant, CancellationToken ct);

    Task UpdateRestaurantAsync(UpdateRestaurantDTO restaurant, CancellationToken ct);
}
