using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCartaAPI.Endpoints.Restaurants.Requests;
using FastEndpoints;
using FluentValidation;

namespace LaCartaAPI.Endpoints.Restaurants.Validators
{
    public class ActivateRestaurantRequestValidator : Validator<ActivateRestaurantRequest>
{
    public ActivateRestaurantRequestValidator()
    {
        RuleFor(x=>x.Id).GreaterThan(0).WithMessage("El Id del restaurante debe ser mayor a 0");
    }
}

}