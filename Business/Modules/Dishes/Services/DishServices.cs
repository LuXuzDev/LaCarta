using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
    // Validar que el restaurante exista
    var restaurant = await _restaurantRepository.GetByIdAsync(idRestaurant, ct);
    if (restaurant == null)
        throw new EntityNotFoundException("Restaurant", idRestaurant);

    // Validar que el nombre sea único dentro del restaurante
    var nameExists = await _dishRepository.IsNameUniqueAsync(dishDTO.Name, idRestaurant);
    if (!nameExists)
        throw new NotUniqueNameException(dishDTO.Name);


    if (dishDTO.Price <= 0)
        throw new ValidationException(
            message: "El precio del plato debe ser mayor que cero."
        );

    //  Mapear DTO a entidad
    var dishEntity = _mapper.Map<Dish>(dishDTO);

    //  Asociar explícitamente al restaurante (desde la ruta)
    dishEntity.RestaurantId = idRestaurant;

    //  Subir imagen si existe
    if (dishDTO.Image != null)
    {
        try
        {
            var imageUrl = await _fileStorageServices.SaveFileAsync(dishDTO.Image);
        }
        catch (Exception ex)
        {
            throw new FileLoadException("Error al subir la imagen del plato.", ex);
        }
    }

    //  Establecer metadatos
    dishEntity.IsActive = true; // Puedes mover esto a una constante si lo usas mucho
    dishEntity.CreatedAt = DateTime.UtcNow;
    dishEntity.UpdatedAt = DateTime.UtcNow;

    // . Guardar en base de datos
    await _dishRepository.AddAsync(dishEntity, ct);

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

    public async Task UpdateDishAsync(UpdateDishDTO dto, CancellationToken ct)
{
    var dish = await _dishRepository.GetByIdAsync(dto.Id, ct);
    if (dish == null)
        throw new EntityNotFoundException("Dish", dto.Id);

    // Validar unicidad de nombre (solo si cambia)
    if (dto.Name != null && dto.Name != dish.Name)
    {
        var nameExists = await _dishRepository.IsNameUniqueAsync(dto.Name, dish.RestaurantId, dto.Id);
        if (!nameExists)
            throw new NotUniqueNameException(dto.Name);
    }

    // Manejo de imagen
    string? currentImage = dish.Image;
    string? newImageUrl = currentImage;

    if (dto.Image != null)
    {
        // Eliminar imagen anterior si existe y no es nula
        if (!string.IsNullOrEmpty(currentImage))
        {
            var imageExists = await _fileStorageServices.FileExistsAsync(currentImage);
            if (imageExists)
                await _fileStorageServices.DeleteFileAsync(currentImage);
        }

        // Subir nueva imagen
        newImageUrl = await _fileStorageServices.SaveFileAsync(dto.Image);
         }

    dish.Image = newImageUrl;

    // Actualizar propiedades (solo si se enviaron)
    if (dto.Name != null) dish.Name = dto.Name;
    if (dto.Price.HasValue) dish.Price = dto.Price.Value;
    if (dto.Description != null) dish.Description = dto.Description;
    if (dto.IsActive.HasValue) dish.IsActive = dto.IsActive.Value;

    dish.UpdatedAt = DateTime.UtcNow; // ← Actualiza timestamp

    // Guardar cambios (usando el mismo patrón que UpdateRestaurantAsync)
    await _dishRepository.UpdateAsync(ct);
}
}
