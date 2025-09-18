using FluentValidation;
using LaCartaAPI.Endpoints.Users.Requests;

namespace LaCartaAPI.Endpoints.Users.Validators;

public class CreateRestaurantManagerRequestValidator : AbstractValidator<CreateRestaurantManagerRequest>
{
    public CreateRestaurantManagerRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es obligatorio.")
            .EmailAddress().WithMessage("El email no tiene un formato válido.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria.")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");
    }
}