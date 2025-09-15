using FastEndpoints;
using LaCartaAPI.Handlers;
using Business.Modules.Dishes.DTOs;
using Business.Modules.Dishes.Services;
using LaCartaAPI.Endpoints.Restaurants.Requests;
using Domain.Modules.Dishs.Enums;
using LaCartaAPI.Endpoints.Dishes.Validators;

namespace LaCartaAPI.Endpoints.Dishes;

public class CreateDishEndpoint : Endpoint<CreateDishRequest, DishDTO>
{
    private readonly IDishServices _dishService;
    private readonly AutoMapper.IMapper _mapper;

    public CreateDishEndpoint(IDishServices dishService, AutoMapper.IMapper mapper)
    {
        _dishService = dishService;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/dishes");
        AllowAnonymous();
        AllowFormData();
        Validator<CreateDishRequestValidator>();
    }

    public override async Task HandleAsync(CreateDishRequest request, CancellationToken ct)
    {
        try
        {
            var dishDto = _mapper.Map<CreateDishDTO>(request);

            await _dishService.CreateDishAsync(dishDto.RestaurantId, dishDto, ct);
            await Send.OkAsync(ct);
        }
        catch (Exception ex)
        {
            await ErrorHandler.HandleExceptionAsync<CreateDishRequest, DishDTO>(ex,Send, ct);
        }
    }
}