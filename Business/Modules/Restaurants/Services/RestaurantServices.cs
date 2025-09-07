using AutoMapper;
using Business.Modules.Restaurants.DTOs;
using Domain.FileStorage;
using Domain.Modules.Restaurants.Enums;
using Domain.Modules.Restaurants.Interfaces;
using Domain.Modules.Restaurants.Models;


namespace Business.Modules.Restaurants.Services;

public class RestaurantServices : IRestaurantServices
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageServices;

    public RestaurantServices(IRestaurantRepository restaurantRepository, IMapper mapper, IFileStorageService fileStorageServices)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
        _fileStorageServices = fileStorageServices;
    }

    public async Task<IEnumerable<RestaurantDTO>> GetRestaurantsActivesAsync(CancellationToken ct)
    {
        var restaurants = await _restaurantRepository.GetRestaurantsActiveAsync();
        return _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
    }

    public async Task<IEnumerable<RestaurantDTO>> GetRestaurantsAsync(CancellationToken ct)
    {
        var restaurants = await _restaurantRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
    }

    public async Task<IEnumerable<RestaurantDTO>> GetRestaurantsByRestaurantManagerIdAsync(int restaurantManagerId, CancellationToken ct)
    {
        var restaurants = await _restaurantRepository.GetByRestaurantsManagerId(restaurantManagerId);
        return _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
    }

    public async Task<RestaurantDTO> GetByIdRestaurantAsync(int restaurantId, CancellationToken ct)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
        return _mapper.Map<RestaurantDTO>(restaurant);
    }

    public async Task<IEnumerable<RestaurantDTO>> GetByRestaurantsCousineTypeAsync(CuisineType cousineType, CancellationToken ct)
    {
        var restaurants = await _restaurantRepository.GetByRestaurantsCousineType(cousineType);
        return _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
    }

    public async Task CreateRestaurantAsync(CreateRestaurantDTO restaurant, CancellationToken ct)
    {
        var restaurantEntity = _mapper.Map<Restaurant>(restaurant);
        restaurantEntity.Image = await _fileStorageServices.SaveFileAsync(restaurant.Image);
        restaurantEntity.IsActive = false;
        restaurantEntity.CreatedAt = DateTime.UtcNow;
        restaurantEntity.UpdatedAt = DateTime.UtcNow;
        await _restaurantRepository.AddAsync(restaurantEntity);
    }
}
