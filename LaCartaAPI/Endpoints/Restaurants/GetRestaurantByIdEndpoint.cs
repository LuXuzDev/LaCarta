using Business.Modules.Restaurants.DTOs;
using Business.Modules.Restaurants.Services;
using Domain.Modules.Shared.Exceptions;
using FastEndpoints;
using LaCartaAPI.Endpoints.Restaurants.Requests;
using LaCartaAPI.Endpoints.Restaurants.Validators;
using LaCartaAPI.Handlers;


public class GetRestaurantByIdEndpoint : Endpoint<GetRestaurantByIdRequest,RestaurantDTO>
{
    private readonly IRestaurantServices _restaurantService;

    public GetRestaurantByIdEndpoint(IRestaurantServices restaurantService)
    {
        _restaurantService = restaurantService;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/restaurants/{id}");
        AllowAnonymous();
        Validator<GetRestaurantByIdRequestValidator>();
    }


    public override async Task HandleAsync(GetRestaurantByIdRequest req,CancellationToken ct)
    {
        try
        {
            var restaurant = await _restaurantService.GetByIdRestaurantAsync(req.Id, ct);
            await Send.OkAsync(restaurant, cancellation: ct);
        }
        catch (EntityNotFoundException ex)
        {
            await ErrorHandler.HandleExceptionAsync<GetRestaurantByIdRequest, RestaurantDTO>(ex, Send, ct);
        }
    }
}

