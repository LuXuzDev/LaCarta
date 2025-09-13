using Business.Modules.Dishes.DTOs;
using Business.Modules.Dishes.Services;
using Domain.Modules.Restaurants.Interfaces;
using FastEndpoints;
using LaCartaAPI.Endpoints.Dishes.Request;

namespace LaCartaAPI.Endpoints.Dishes;

public class GetDishesByRestaurantEndpoint : Endpoint<GetDishesRequest, IEnumerable<DishDTO>>
{
    private readonly IDishServices _dishService;
    private readonly IRestaurantRepository _restaurantRepository;

    public GetDishesByRestaurantEndpoint(
        IDishServices dishService,
        IRestaurantRepository restaurantRepository)
    {
        _dishService = dishService;
        _restaurantRepository = restaurantRepository;
    }

    public override void Configure()
    {
        Get("/restaurants/{restaurantId}/dishes");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetDishesRequest req, CancellationToken ct)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(req.RestaurantId, ct);

        if (restaurant != null)
        {
            var dishes = await _dishService.GetByRestaurantIdAsync(req.RestaurantId, ct);
            await Send.OkAsync(dishes, ct);
        }
        else
        {
            await Send.NotFoundAsync(ct); 
        }
    }
}