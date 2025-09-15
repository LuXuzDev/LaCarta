using Domain.Modules.Dishs.Models;


namespace Domain.Modules.Dishs.Interfaces;

public interface IDishRepository
{
    // Consultas
    Task<Dish> GetByIdAsync(int dishId, CancellationToken ct);
    Task<IEnumerable<Dish>> GetByRestaurantIdAsync(int restaurantId, CancellationToken ct);

    //Validacion
    Task<bool> IsNameUniqueAsync(string name, int restaurantId, int? excludeDishId = null);

    // Comandos
    Task AddAsync(Dish dish, CancellationToken ct);
    Task UpdateAsync( CancellationToken ct);
    Task ToggleStateAsync(int id, CancellationToken ct);


}
