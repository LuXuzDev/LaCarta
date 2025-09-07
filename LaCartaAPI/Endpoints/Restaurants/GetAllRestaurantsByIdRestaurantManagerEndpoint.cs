using Business.Modules.Restaurants.DTOs;
using Business.Modules.Restaurants.Services;
using FastEndpoints;
using LaCartaAPI.Endpoints.Restaurants.Requests;
using LaCartaAPI.Endpoints.Restaurants.Validators;
using LaCartaAPI.Handlers;

namespace LaCartaAPI.Endpoints.Restaurants;

public class GetAllRestaurantsByIdRestaurantManagerEndpoint : Endpoint<GetAllRestaurantsByIdRestaurantManagerRequest, IEnumerable<RestaurantDTO>>
{
    private readonly IRestaurantServices _restaurantService;

    public GetAllRestaurantsByIdRestaurantManagerEndpoint(IRestaurantServices restaurantService)
    {
        _restaurantService = restaurantService;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/restaurants/restaurant-manager/{id}");
        AllowAnonymous();
        Validator<GetAllRestaurantsByIdRestaurantManagerRequestValidator>();
    }

    public override async Task HandleAsync(GetAllRestaurantsByIdRestaurantManagerRequest req,CancellationToken ct)
    {
        try
        {
            var restaurants = await _restaurantService.GetRestaurantsByRestaurantManagerIdAsync(req.Id,ct);
            await Send.OkAsync(restaurants, cancellation: ct);
        }
        catch (Exception ex)
        {
            await ErrorHandler.HandleExceptionAsync<GetAllRestaurantsByIdRestaurantManagerRequest, IEnumerable<RestaurantDTO>>(ex, Send, ct);
        }
    }
}