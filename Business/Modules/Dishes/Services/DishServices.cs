using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Modules.Dishes.DTOs;
using Domain.FileStorage;
using Domain.Modules.Dishs.Interfaces;
using Domain.Modules.Dishs.Models;
using Domain.Modules.Restaurants.Exceptions;
using Domain.Modules.Restaurants.Interfaces;
using Domain.Modules.Shared.Exceptions;
using Domain.Modules.Users.Interfaces;

namespace Business.Modules.Dishes.Services;


public class DishServices : IDishServices
{

    private readonly IDishRepository _dishRepository;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageServices;
    private readonly IRestaurantRepository _restaurantRepository;

    public DishServices
            (IDishRepository dishRepository, IMapper mapper, IFileStorageService fileStorageServices,
             IRestaurantRepository restaurantRepository)
    {
        _dishRepository = dishRepository;
        _mapper = mapper;
        _fileStorageServices = fileStorageServices;
        _restaurantRepository = restaurantRepository;
    }

    public async Task CreateDishAsync(int idRestaurant, CreateDishDTO dishDTO, CancellationToken ct)
    {
        {
            //  Validación del restaurante
            var restaurant = await _restaurantRepository.GetByIdAsync(idRestaurant, ct);
            if (restaurant == null)
                throw new EntityNotFoundException("Restaurant", idRestaurant);


            var nameExists = await _dishRepository.IsNameUniqueAsync(dishDTO.Name, idRestaurant);
            if (!nameExists)
                throw new NotUniqueNameException(dishDTO.Name);

            //  Mapear DTO a entidad
            var dishEntity = _mapper.Map<Dish>(dishDTO);

            //  Subir imagen si existe
            if (dishDTO.Image != null)
            {
                var imageUrl = await _fileStorageServices.SaveFileAsync(dishDTO.Image);
                dishEntity.Image = imageUrl;
            }

            dishEntity.IsActive = false;
            dishEntity.CreatedAt = DateTime.UtcNow;
            dishEntity.UpdatedAt = DateTime.UtcNow;
            await _dishRepository.AddAsync(dishEntity);
        }
    }
    public async Task<DishDTO> GetByIdAsync(int dishId, CancellationToken ct)
{
    var dish = await _dishRepository.GetByIdAsync(dishId, ct);

    if (dish == null)
        throw new EntityNotFoundException("Dish", dishId);

    return _mapper.Map<DishDTO>(dish);
}

   public async Task<IEnumerable<DishDTO>> GetByRestaurantIdAsync(int restaurantId, CancellationToken ct)
{
    var dishes = await _dishRepository.GetByRestaurantIdAsync(restaurantId, ct);

    // Filtrar solo activos
    var activeDishes = dishes.Where(d => d.IsActive);

    var dishDTOS = _mapper.Map<IEnumerable<DishDTO>>(activeDishes);

    return dishDTOS;
}

    public Task UpdateDishAsync(UpdateDishDTO dish, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
