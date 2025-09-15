using AutoMapper;
using Business.Modules.Restaurants.DTOs;
using Domain.Modules.Restaurants.Models;


namespace Business.Modules.Restaurants.Mappers;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Restaurant, RestaurantDTO>()
            .ForMember(
                dest => dest.RestaurantTags,
                opt => opt.MapFrom(src => src.RestaurantTags.Select(tag => tag.ToString()).ToList())
            )
        .ForMember(
    dest => dest.Image,
    opt => opt.MapFrom(src => src.Image != null
        ? $"/{src.Image.Replace("\\", "/")}"
        : null)
);
        CreateMap<CreateRestaurantDTO, Restaurant>().ReverseMap();
    }
}
