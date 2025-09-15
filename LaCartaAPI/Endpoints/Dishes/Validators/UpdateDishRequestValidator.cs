using FluentValidation;
using LaCartaAPI.Endpoints.Dishes.Request;
using Microsoft.AspNetCore.Http;

namespace LaCartaAPI.Validators.Dishes;

public class UpdateDishRequestValidator : AbstractValidator<UpdateDishRequest>
{
    public UpdateDishRequestValidator()
    {
        //  ID obligatorio y válido
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("El ID del plato debe ser un número positivo.");

        //  Name: opcional, pero si se envía, debe ser válido
        RuleFor(x => x.Name)
            .MaximumLength(100)
            .WithMessage("El nombre del plato no puede exceder los 100 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Name)); // Solo si se envía

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("El nombre del plato no puede estar vacío.")
            .When(x => x.Name is not null); // Si se envía, no puede ser solo espacio

        //  Price: si se envía, debe ser mayor a 0
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("El precio del plato debe ser mayor que cero.")
            .When(x => x.Price.HasValue);

        //  Description: opcional, pero si se envía, máximo 500 caracteres
        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("La descripción del plato no puede exceder los 500 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        //Image: si se envía y validar tipo 
        RuleFor(x => x.Image)
            .Must(BeValidImage)
            .WithMessage("Solo se permiten imágenes en formato JPEG, JPG o PNG.")
            .When(x => x.Image != null);
}

    private bool BeValidImage(IFormFile? file)
    {
        if (file == null) return true;

        var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png" };
        return allowedTypes.Contains(file.ContentType.ToLowerInvariant());
    }
}