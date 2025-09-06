using AutoMapper;
using Business.Modules.Restaurants.DTOs;
using Domain.Modules.Restaurants.Interfaces;

namespace Business.Modules.Restaurants.Services;

public class RestaurantServices : IRestaurantServices
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;


    public RestaurantServices(IRestaurantRepository restaurantRepository, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        this._mapper = mapper;
    }


    public async Task<IEnumerable<RestaurantDTO>> GetAllRestaurantsAsync(CancellationToken ct)
    {
        var restaurants = await _restaurantRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
    }

    public async Task<RestaurantDTO> GetByIdRestaurantAsync(int restaurantId,CancellationToken ct)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);

        return _mapper.Map<RestaurantDTO>(restaurant);
    }
}
