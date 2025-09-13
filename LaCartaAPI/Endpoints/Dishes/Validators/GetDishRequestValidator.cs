using FastEndpoints;
using FluentValidation;
using LaCartaAPI.Endpoints.Dishes.Requests;

namespace LaCartaAPI.Endpoints.Dishes.Validators;

public class GetDishRequestValidator : Validator<GetDishRequest>
{
    public GetDishRequestValidator()
    {
        RuleFor(x => x.DishId)
            .GreaterThan(0)
            .WithMessage("El ID del plato debe ser un número entero positivo.");
    }
}