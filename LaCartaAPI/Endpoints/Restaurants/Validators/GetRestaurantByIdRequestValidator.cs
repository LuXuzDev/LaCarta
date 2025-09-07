using FastEndpoints;
using FluentValidation;
using LaCartaAPI.Endpoints.Restaurants.Requests;

namespace LaCartaAPI.Endpoints.Restaurants.Validators;

public class GetRestaurantByIdRequestValidator : Validator<GetRestaurantByIdRequest>
{
    public GetRestaurantByIdRequestValidator()
    {
        RuleFor(x=>x.Id).GreaterThan(0).WithMessage("El Id del restaurante debe ser mayor a 0");
    }
}
