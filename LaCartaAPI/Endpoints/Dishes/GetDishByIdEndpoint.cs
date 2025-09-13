using Business.Modules.Dishes.DTOs;
using Business.Modules.Dishes.Services;
using FastEndpoints;
using LaCartaAPI.Endpoints.Dishes.Requests;

public class GetDishByIdEndpoint : Endpoint<GetDishRequest, DishDTO>
{
    private readonly IDishServices _dishService;

    public GetDishByIdEndpoint(IDishServices dishService) => _dishService = dishService;

    public override void Configure()
    {
        Get("/dishes/{dishId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetDishRequest req, CancellationToken ct)
    {
        var dishDto = await _dishService.GetByIdAsync(req.DishId, ct);
        await Send.OkAsync(dishDto, ct);
    }
}