using Domain.Modules.Users.Models;


namespace Domain.Modules.Users.Interfaces;

public interface IUserRepository
{
    // Consultas
    Task<IEnumerable<User>> GetAllAsync(CancellationToken ct);
    Task<User?> GetByIdAsync(int userId, CancellationToken ct);
    Task<User?> GetByEmailAsync(string email, CancellationToken ct);


    //Validacion
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken ct, int? excludeUserId = null );

    // Comandos
    Task AddAsync(User user, CancellationToken ct);
    Task UpdateAsync(User user, CancellationToken ct);
}
