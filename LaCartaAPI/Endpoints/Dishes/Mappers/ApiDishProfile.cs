using AutoMapper;
using LaCartaAPI.Endpoints.Restaurants.Requests;
using Business.Modules.Restaurants.DTOs;
using Business.Modules.Dishes.DTOs;
using LaCartaAPI.Endpoints.Dishes.Request;

public class ApiDishProfile : Profile
{
    public ApiDishProfile()
    {
        CreateMap<CreateDishRequest, CreateDishDTO>();
        CreateMap<UpdateDishRequest, UpdateRestaurantDTO>();
    }
}
