using Business.Modules.Restaurants.DTOs;
using Business.Modules.Restaurants.Services;
using FastEndpoints;
using LaCartaAPI.Endpoints.Restaurants.Requests;
using LaCartaAPI.Handlers;

namespace LaCartaAPI.Endpoints.Restaurants;

public class UpdateRestaurantEndpoint : Endpoint<UpdateRestaurantRequest, RestaurantDTO>
{
    private readonly IRestaurantServices _restaurantService;
    private readonly AutoMapper.IMapper _mapper;

    public UpdateRestaurantEndpoint(IRestaurantServices restaurantService, AutoMapper.IMapper mapper)
    {
        _restaurantService = restaurantService;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Verbs(Http.PUT);
        Routes("/restaurants");
        AllowAnonymous();
        AllowFormData();
        Validator<UpdateRestaurantRequestValidator>();
    }

    public override async Task HandleAsync(UpdateRestaurantRequest request, CancellationToken ct)
    {
        try
        {
            var requestDto = _mapper.Map<UpdateRestaurantDTO>(request);
            await _restaurantService.UpdateRestaurantAsync(requestDto, ct);
            await Send.OkAsync(cancellation: ct);
        }
        catch (Exception ex)
        {
            await ErrorHandler.HandleExceptionAsync<UpdateRestaurantRequest, RestaurantDTO>(ex, Send, ct);
        }
    }
}
