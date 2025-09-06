using Domain.Modules.Dishs.Models;


namespace Domain.Modules.Dishs.Interfaces;

public interface IDishRepository
{
    // Consultas
    Task<IEnumerable<Dish>> GetAllAsync();
    Task<Dish?> GetByIdAsync(int id);
    Task<Dish?> GetByNameAsync(string name, int restaurantId);
    Task<IEnumerable<Dish>> GetByRestaurantIdAsync(int restaurantId);

    //Validacion
    Task<bool> IsNameUniqueAsync(string name, int restaurantId, int? excludeDishId = null);

    // Comandos
    Task AddAsync(Dish dish);
    Task UpdateAsync(Dish dish);

    // Gestión de estado 
    Task ActivateAsync(int dishId);
    Task DeactivateAsync(int dishId);
}
