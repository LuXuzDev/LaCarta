using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
        using FluentValidation;
using LaCartaAPI.Endpoints.Restaurants.Requests;
using Domain.Modules.Dishs.Enums;

namespace LaCartaAPI.Endpoints.Dishes.Validators
{
    public class CreateDishRequestValidator : AbstractValidator<CreateDishRequest>
{
    public CreateDishRequestValidator()
    {
        // Name
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(255).WithMessage("El nombre no puede superar los 255 caracteres.");

        // Image
        RuleFor(x => x.Image)
            .NotNull().WithMessage("La imagen es obligatoria.")
            .Must(BeAValidImage).WithMessage("El archivo debe ser un formato de imagen valido (jpg, png, webp).");

        // Price
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("El precio debe ser mayor a cero.");

        // Description
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripcion es obligatoria.")
            .MaximumLength(2000).WithMessage("La descripcion no puede superar los 2000 caracteres.");

        // DishType
        RuleFor(x => x.DishType)
            .IsInEnum().WithMessage("El tipo de plato especificado no es valido.");
    }

    private bool BeAValidImage(IFormFile file)
    {
        if (file == null) return false;

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        return allowedExtensions.Contains(extension) && file.Length > 0;
    }
}
    }
