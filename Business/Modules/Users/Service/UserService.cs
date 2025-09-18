using AutoMapper;
using Business.Modules.Users.DTOs;
using Domain.Modules.Security.Interfaces;
using Domain.Modules.Shared.Exceptions;
using Domain.Modules.Users.Interfaces;
using Domain.Modules.Users.Models;

namespace Business.Modules.Users.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordService _passwordService;


    public UserService(IUserRepository userRepository, IMapper mapper,
        IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordService = passwordService;
    }


    public async Task CreateUserAsync(CreateUserDTO user, CancellationToken ct)
    {
        await ValidateUniqueEmail(user.Email, ct);

        var userEntity = _mapper.Map<User>(user);
        userEntity.RoleId = 1; // Asignar rol de RM por defecto 
        userEntity.Password = _passwordService.Hash(user.Password);

        userEntity.UpdatedAt = DateTime.UtcNow;
        userEntity.CreatedAt = DateTime.UtcNow;

        await _userRepository.AddAsync(userEntity, ct);
    }


    public async Task<UserDTO> GetByIdUserAsync(int userId, CancellationToken ct)
    {
        var user = await _userRepository.GetByIdAsync(userId, ct);

        if (user == null)
            throw new EntityNotFoundException("User", userId);
        return _mapper.Map<UserDTO>(user);
    }


    private async Task ValidateUniqueEmail(string email, CancellationToken ct, int? userId = null)
    {
        if (!await _userRepository.IsEmailUniqueAsync(email, ct, userId))
            throw new NotUniqueEmailException(email);
    }
}
