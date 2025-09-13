using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using FluentValidation;
using LaCartaAPI.Endpoints.Dishes.Request;

namespace LaCartaAPI.Endpoints.Dishes.Validators
{
    public class GetDishesRequestValidator: Validator<GetDishesRequest>
{
    public GetDishesRequestValidator()
    {
        RuleFor(x => x.RestaurantId)
            .GreaterThan(0)
            .WithMessage("El ID del restaurante debe ser un número entero positivo.");
    }
        
    }
}