using Business.Modules.Restaurants.DTOs;
using Business.Modules.Restaurants.Services;
using FastEndpoints;
using LaCartaAPI.Endpoints.Restaurants.Requests;
using LaCartaAPI.Handlers;


namespace LaCartaAPI.Endpoints.Restaurants;

public class CreateRestaurantEndpoint : Endpoint<CreateRestaurantRequest, RestaurantDTO>
{
    private readonly IRestaurantServices _restaurantService;
    private readonly AutoMapper.IMapper _mapper;

    public CreateRestaurantEndpoint(IRestaurantServices restaurantService, AutoMapper.IMapper mapper)
    {
        _restaurantService = restaurantService;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/restaurants");
        AllowAnonymous();
        AllowFormData();
    }

    public override async Task HandleAsync(CreateRestaurantRequest request, CancellationToken ct)
    {
        try
        {
            var requestDto = _mapper.Map<CreateRestaurantDTO>(request);
            await _restaurantService.CreateRestaurantAsync(requestDto, ct);
            await Send.OkAsync(cancellation: ct);
        }
        catch (Exception ex)
        {
            await ErrorHandler.HandleExceptionAsync<CreateRestaurantRequest, RestaurantDTO>(ex, Send, ct);
        }
    }
}