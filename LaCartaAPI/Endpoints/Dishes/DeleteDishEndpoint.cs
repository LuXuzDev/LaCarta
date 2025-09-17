using FastEndpoints;
using Business.Modules.Dishes.DTOs;
using Business.Modules.Dishes.Services;
using LaCartaAPI.Endpoints.Dishes.Requests;
using LaCartaAPI.Handlers;

namespace LaCartaAPI.Endpoints.Dishes;

public class DeleteDishEndpoint : Endpoint<DeleteDishDTO, DeleteDishResponse>
{
    private readonly IDishServices _dishService;
    private readonly AutoMapper.IMapper _mapper;

    public DeleteDishEndpoint(IDishServices dishService, AutoMapper.IMapper mapper)
    {
        _dishService = dishService;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Verbs(Http.DELETE);
        Routes("/dishes/{id}");
        AllowAnonymous();

    }
    public override async Task HandleAsync(DeleteDishDTO req, CancellationToken ct)
    {
        try
        {
            var response = await _dishService.DeleteDishAsync(req, ct);
            await Send.OkAsync(response, cancellation: ct);
        }
        catch (Exception ex)
        {
            await ErrorHandler.HandleExceptionAsync<DeleteDishDTO, DeleteDishResponse>(ex, Send, ct);
        }
    }
}