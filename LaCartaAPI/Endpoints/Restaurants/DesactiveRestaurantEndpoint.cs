using Business.Modules.Restaurants.Services;
using Domain.Modules.Shared.Exceptions;
using FastEndpoints;
using LaCartaAPI.Handlers;

namespace LaCartaAPI.Endpoints.Restaurants;

public class DesactivateRestaurantEndpoint : EndpointWithoutRequest
{
    private readonly IRestaurantServices _restaurantService;

    public DesactivateRestaurantEndpoint(IRestaurantServices restaurantService)
    {
        _restaurantService = restaurantService;
    }

    public override void Configure()
    {
        Patch("/restaurants/desactivate-restaurant/{id}/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        try
        {
            var id = Route<int>("id");
            await _restaurantService.DesactivateRestaurantAsync(id, ct);
            await Send.OkAsync("Restaurante desactivado correctamente", ct);
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