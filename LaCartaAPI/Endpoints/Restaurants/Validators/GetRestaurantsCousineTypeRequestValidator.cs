using FastEndpoints;
using LaCartaAPI.Endpoints.Restaurants.Requests;
using FluentValidation;

namespace LaCartaAPI.Endpoints.Restaurants.Validators;

public class GetRestaurantsCousineTypeRequestValidator:Validator<GetRestaurantsCousineTypeRequest>
{
    public GetRestaurantsCousineTypeRequestValidator() 
    {
        RuleFor(x => x.cousineType).IsInEnum().WithMessage("El tipo de cocina no es valido");
    }
}
