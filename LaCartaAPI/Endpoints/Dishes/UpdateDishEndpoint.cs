
using Business.Modules.Dishes.DTOs;
using Business.Modules.Dishes.Services;
using FastEndpoints;
using LaCartaAPI.Endpoints.Dishes.Request;
using LaCartaAPI.Validators.Dishes;

namespace LaCartaAPI.Endpoints.Dishes;
    public class UpdateDishEndpoint : Endpoint<UpdateDishRequest, DishDTO>
{
    private readonly IDishServices _dishService;
    private readonly AutoMapper.IMapper _mapper;

    public UpdateDishEndpoint(IDishServices dishService, AutoMapper.IMapper mapper)
    {
        _dishService = dishService;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Verbs(Http.PUT);
        Routes("/dishes");
        AllowAnonymous();
        AllowFormData(); 
        Validator<UpdateDishRequestValidator>();
    }

    public override async Task HandleAsync(UpdateDishRequest request, CancellationToken ct)
    {
        var dto = _mapper.Map<UpdateDishDTO>(request);
        await _dishService.UpdateDishAsync(dto, ct);

        var updatedDish = await _dishService.GetByIdAsync(dto.Id, ct);
        await Send.OkAsync(updatedDish, cancellation: ct);
    }
}