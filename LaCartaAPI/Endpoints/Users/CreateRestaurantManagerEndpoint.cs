using Business.Modules.Users.DTOs;
using Business.Modules.Users.Service;
using FastEndpoints;
using LaCartaAPI.Endpoints.Users.Requests;
using LaCartaAPI.Endpoints.Users.Validators;
using LaCartaAPI.Handlers;

namespace LaCartaAPI.Endpoints.Users;

public class CreateRestaurantManagerEndpoint : Endpoint<CreateRestaurantManagerRequest>
{
    private readonly IUserService _userService;
    private readonly AutoMapper.IMapper _mapper;

    public CreateRestaurantManagerEndpoint(IUserService userService, AutoMapper.IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Post("/users/restaurant-manager");
        AllowAnonymous();
        Validator<CreateRestaurantManagerRequestValidator>();
    }


    public override async Task HandleAsync(CreateRestaurantManagerRequest req, CancellationToken ct)
    {
        try
        {
            var requestDto = _mapper.Map<CreateUserDTO>(req);
            await _userService.CreateUserAsync(requestDto, ct);
            await Send.OkAsync(new { Message = "Manager creado correctamente." }, ct);
        }
        catch (Exception ex)
        {
            await ErrorHandler.HandleExceptionAsync<CreateRestaurantManagerRequest, object>(ex, Send, ct);
        }
    }
}
