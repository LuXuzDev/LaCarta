using Domain.Modules.Restaurants.Enums;
using Domain.Modules.Restaurants.Models;


namespace Domain.Modules.Restaurants.Interfaces;

public interface IRestaurantRepository
{
    // Consultas
    Task<IEnumerable<Restaurant>> GetAllAsync(CancellationToken ct);
    Task<Restaurant?> GetByIdAsync(int restaurantId, CancellationToken ct);
    Task<Restaurant?> GetByEmailAsync(string email, CancellationToken ct);
    Task<IEnumerable<Restaurant>> GetByRestaurantsManagerId(int managerId, CancellationToken ct);
    Task<IEnumerable<Restaurant>> GetRestaurantsActiveAsync(CancellationToken ct);
    Task<IEnumerable<Restaurant>> GetByRestaurantsCousineType(CuisineType cousineTypeId, CancellationToken ct);


    //Validacion
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken ct, int? excludeRestaurantId = null);
    Task<bool> IsNameUniqueAsync(string name, CancellationToken ct, int? excludeRestaurantId = null);
    Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber, CancellationToken ct, int? excludeRestaurantId = null);

    // Comandos
    Task AddAsync(Restaurant restaurant, CancellationToken ct);
    Task UpdateAsync(CancellationToken ct);
}
