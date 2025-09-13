using Business.Modules.Dishes.DTOs;

namespace Business.Modules.Dishes.Services;
    public interface IDishServices
    {

    Task<DishDTO> GetByIdAsync(int dishId, CancellationToken ct);
    Task<IEnumerable<DishDTO>> GetByRestaurantIdAsync(int restaurantId, CancellationToken ct);
    Task CreateDishAsync(int idRestaurant, CreateDishDTO dish, CancellationToken ct);
    Task UpdateDishAsync(UpdateDishDTO dish, CancellationToken ct);

    }