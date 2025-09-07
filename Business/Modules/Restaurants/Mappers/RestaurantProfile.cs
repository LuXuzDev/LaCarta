using AutoMapper;
using Business.Modules.Restaurants.DTOs;
using Domain.Modules.Restaurants.Models;


namespace Business.Modules.Restaurants.Mappers;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Restaurant, RestaurantDTO>().ReverseMap();
        CreateMap<CreateRestaurantDTO,Restaurant>().ReverseMap();
    }
}
