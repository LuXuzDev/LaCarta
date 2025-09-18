using Business.Modules.Users.DTOs;

namespace Business.Modules.Users.Service;

public interface IUserService
{
    Task<UserDTO> GetByIdUserAsync(int userId, CancellationToken ct);

    Task CreateUserAsync(CreateUserDTO user, CancellationToken ct);
}
