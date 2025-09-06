using Domain.Modules.Restaurants.Models;


namespace Domain.Modules.Restaurants.Interfaces;

public interface IRestaurantRepository
{
    // Consultas
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(int id);
    Task<Restaurant?> GetByEmailAsync(string email);


    //Validacion
    Task<bool> IsEmailUniqueAsync(string email, int? excludeRestaurantId = null);
    Task<bool> IsNameUniqueAsync(string name, int? excludeRestaurantId = null);

    // Comandos
    Task AddAsync(Restaurant restaurant);
    Task UpdateAsync(Restaurant restaurant);

    // Gestión de estado 
    Task ActivateAsync(int restaurantId);
    Task DeactivateAsync(int restaurantId);
}
