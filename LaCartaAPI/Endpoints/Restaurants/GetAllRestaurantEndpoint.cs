using Business.Modules.Restaurants.DTOs;
using Business.Modules.Restaurants.Services;
using FastEndpoints;
using LaCartaAPI.Handlers;

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
        Verbs(Http.GET);
        Routes("/restaurants");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        try
        {
            var restaurants = await _restaurantService.GetAllRestaurantsAsync(ct);
            await Send.OkAsync(restaurants, cancellation: ct);
        }
        catch(Exception ex)
        {
            await ErrorHandler.HandleExceptionAsync<EmptyRequest, IEnumerable<RestaurantDTO>>(ex, Send, ct);

        }

    }
}