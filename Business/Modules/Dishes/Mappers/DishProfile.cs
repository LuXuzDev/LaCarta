
using AutoMapper;
using Business.Modules.Dishes.DTOs;
using Domain.Modules.Dishs.Models;

namespace Business.Modules.Dishes.Mappers;
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<Dish, DishDTO>().ReverseMap();
            CreateMap<CreateDishDTO, Dish>();
        }
    }
