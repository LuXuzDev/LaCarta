using Business.Modules.Restaurants.Services;
using Domain.Modules.Shared.Exceptions;
using FastEndpoints;
using LaCartaAPI.Handlers;

namespace LaCartaAPI.Endpoints.Restaurants;

public class ActivateRestaurantEndpoint : EndpointWithoutRequest
{
    private readonly IRestaurantServices _restaurantService;

    public ActivateRestaurantEndpoint(IRestaurantServices restaurantService)
    {
        _restaurantService = restaurantService;
    }

    public override void Configure()
    {
        Patch("/restaurants/activate-restaurant/{id}/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        try
        {
            var id = Route<int>("id");
            await _restaurantService.ActivateRestaurantAsync(id, ct);
            await Send.OkAsync("Restaurante activado correctamente", ct);
        }
        catch (EntityNotFoundException ex)
        {
            await ErrorHandler.HandleExceptionAsync<EmptyRequest, object>(ex, Send, ct);
        }
        catch (Exception ex)
        {
            await ErrorHandler.HandleExceptionAsync<EmptyRequest, object>(ex, Send, ct);
        }
    }
}