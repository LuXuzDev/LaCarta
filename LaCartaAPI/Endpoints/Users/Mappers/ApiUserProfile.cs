using AutoMapper;
using Business.Modules.Users.DTOs;
using LaCartaAPI.Endpoints.Users.Requests;

namespace LaCartaAPI.Endpoints.Users.Mappers;

public class ApiUserProfile : Profile
{
    public ApiUserProfile()
    {
        CreateMap<CreateRestaurantManagerRequest, CreateUserDTO>().ReverseMap();
    }
}
