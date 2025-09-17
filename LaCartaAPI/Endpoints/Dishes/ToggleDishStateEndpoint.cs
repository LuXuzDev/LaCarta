
using FastEndpoints;
using Business.Modules.Dishes.DTOs;
using Business.Modules.Dishes.Services;
using LaCartaAPI.Endpoints.Dishes.Requests;
using LaCartaAPI.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace LaCartaAPI.Endpoints.Dishes;

[Consumes("application/json")] 
public class ToggleDishStateEndpoint : Endpoint<ToggleDishStateRequest, ToggleDishResponse>
{
    private readonly IDishServices _dishService;
    private readonly AutoMapper.IMapper _mapper;

    public ToggleDishStateEndpoint(IDishServices dishService, AutoMapper.IMapper mapper)
    {
        _dishService = dishService;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Verbs(Http.PATCH);
        Routes("/dishes/{id}/toggle");
        AllowAnonymous();

    }

    public override async Task HandleAsync(ToggleDishStateRequest req, CancellationToken ct)
    {
        try
        {
            // Mapea Request (de ruta) a DTO (para servicio)
            var toggleDto = _mapper.Map<ToggleDishDTO>(req);

            // Llama al servicio para cambiar el estado
            var response = await _dishService.ToggleDishStateAsync(toggleDto, ct);

            // Devuelve el nuevo estado
            await Send.OkAsync(response, cancellation: ct);
        }
        catch (Exception ex)
        {
            await ErrorHandler.HandleExceptionAsync<ToggleDishStateRequest, ToggleDishResponse>(ex, Send, ct);
        }
    }
}