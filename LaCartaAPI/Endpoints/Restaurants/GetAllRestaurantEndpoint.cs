using Business.Modules.Restaurants.DTOs;
using Business.Modules.Restaurants.Services;
using FastEndpoints;

namespace LaCartaAPI.Endpoints.Restaurants;

public class GetAllRestaurantsEndpoint : EndpointWithoutRequest<IEnumerable<RestaurantDTO>>
{
    private readonly IRestaurantServices _restaurantService;

    public GetAllRestaurantsEndpoint(IRestaurantServices restaurantService)
    {
        _restaurantService = restaurantService;
    }

    public override void Configure()
    {
        Get("/api/restaurants");
        AllowAnonymous();
        Description(d => d
            .WithSummary("Obtener todos los restaurantes")
            .WithDescription("Devuelve una lista de todos los restaurantes activos e inactivos."));
        Version(1);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var restaurants = await _restaurantService.GetAllRestaurantsAsync(ct);
        await Send.OkAsync(restaurants, cancellation: ct);
    }
}