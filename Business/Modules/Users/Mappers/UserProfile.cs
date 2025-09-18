using AutoMapper;
using Business.Modules.Users.DTOs;
using Domain.Modules.Users.Models;

namespace Business.Modules.Users.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
         CreateMap<User, UserDTO>().ReverseMap();
         CreateMap<CreateUserDTO, User>().ReverseMap();
    }
}
