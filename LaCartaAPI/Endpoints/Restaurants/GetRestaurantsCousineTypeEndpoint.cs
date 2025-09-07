using Business.Modules.Restaurants.DTOs;
using Business.Modules.Restaurants.Services;
using FastEndpoints;
using LaCartaAPI.Endpoints.Restaurants.Requests;
using LaCartaAPI.Endpoints.Restaurants.Validators;
using LaCartaAPI.Handlers;

namespace LaCartaAPI.Endpoints.Restaurants;

public class GetRestaurantsCousineTypeEndpoint : Endpoint<GetRestaurantsCousineTypeRequest, IEnumerable<RestaurantDTO>>
{
    private readonly IRestaurantServices _restaurantService;

    public GetRestaurantsCousineTypeEndpoint(IRestaurantServices restaurantService)
    {
        _restaurantService = restaurantService;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/restaurants/restaurants-cousine-type");
        AllowAnonymous();
        Validator<GetRestaurantsCousineTypeRequestValidator>();
    }

    public override async Task HandleAsync(GetRestaurantsCousineTypeRequest req,CancellationToken ct)
    {
        try
        {
            var restaurants = await _restaurantService.GetByRestaurantsCousineTypeAsync(req.cousineType,ct);
            await Send.OkAsync(restaurants, cancellation: ct);
        }
        catch (Exception ex)
        {
            await ErrorHandler.HandleExceptionAsync<GetRestaurantsCousineTypeRequest, IEnumerable<RestaurantDTO>>(ex, Send, ct);

        }

    }
}