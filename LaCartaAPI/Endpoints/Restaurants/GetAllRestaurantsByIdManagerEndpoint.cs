using Business.Modules.Restaurants.DTOs;
using Business.Modules.Restaurants.Services;
using FastEndpoints;
using LaCartaAPI.Endpoints.Restaurants.Requests;
using LaCartaAPI.Endpoints.Restaurants.Validators;
using LaCartaAPI.Handlers;

namespace LaCartaAPI.Endpoints.Restaurants;

public class GetAllRestaurantsByIdManagerEndpoint : Endpoint<GetAllRestaurantsByIdManagerRequest, IEnumerable<RestaurantDTO>>
{
    private readonly IRestaurantServices _restaurantService;

    public GetAllRestaurantsByIdManagerEndpoint(IRestaurantServices restaurantService)
    {
        _restaurantService = restaurantService;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/restaurants/manager/{id}");
        AllowAnonymous();
        Validator<GetAllRestaurantsByIdManagerRequestValidator>();
    }

    public override async Task HandleAsync(GetAllRestaurantsByIdManagerRequest req,CancellationToken ct)
    {
        try
        {
            var restaurants = await _restaurantService.GetRestaurantsByManagerIdAsync(req.Id,ct);
            await Send.OkAsync(restaurants, cancellation: ct);
        }
        catch (Exception ex)
        {
            await ErrorHandler.HandleExceptionAsync<GetAllRestaurantsByIdManagerRequest, IEnumerable<RestaurantDTO>>(ex, Send, ct);
        }
    }
}