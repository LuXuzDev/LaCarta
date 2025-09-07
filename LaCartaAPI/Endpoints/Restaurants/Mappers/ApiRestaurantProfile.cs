using AutoMapper;
using LaCartaAPI.Endpoints.Restaurants.Requests;
using Business.Modules.Restaurants.DTOs;

public class ApiRestaurantProfile : Profile
{
    public ApiRestaurantProfile()
    {
        CreateMap<CreateRestaurantRequest, CreateRestaurantDTO>();
    }
}
