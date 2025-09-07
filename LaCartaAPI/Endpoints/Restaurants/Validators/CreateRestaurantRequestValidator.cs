using FluentValidation;
using LaCartaAPI.Endpoints.Restaurants.Requests;
using System.Data;

public class CreateRestaurantRequestValidator : AbstractValidator<CreateRestaurantRequest>
{
    private const int MaxFileSize = 5 * 1024 * 1024; // 5 MB
    private static readonly string[] ValidImageTypes = new[] { "image/jpeg", "image/jpg", "image/png" };

    public CreateRestaurantRequestValidator()
    {
        // Nombre: requerido, al menos 2 caracteres, máximo 100
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del restaurante es obligatorio.")
            .Length(1, 100).WithMessage("El nombre debe tener entre 1 y 100 caracteres.");

        // Email: requerido, formato válido, máximo 256 caracteres
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
            .EmailAddress().WithMessage("El correo electrónico no tiene un formato válido.")
            .MaximumLength(256).WithMessage("El correo electrónico no debe exceder los 256 caracteres.");

        // Teléfono: requerido, formato básico (puedes ajustarlo a tu país)
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("El número de teléfono es obligatorio.")
            .Matches(@"^\d{8}$")
                .WithMessage("El número de teléfono debe ser numérico y tener exactamente 8 dígitos.");

        RuleFor(x => x.Image)
           .NotNull().WithMessage("La imagen es obligatoria.");

        RuleFor(x // Validación de IFormFile (imagen obligatoria)
            => x.Image)
            .Must(BeValidImageFile)
            .WithMessage("La imagen debe ser un archivo JPG o PNG válido y no debe exceder los 5 MB.");

        // Horario: la hora de apertura debe ser anterior a la de cierre
        RuleFor(x => x.CloseHour)
            .GreaterThan(x => x.OpenHour)
            .WithMessage("La hora de cierre debe ser posterior a la hora de apertura.");

        // UserId y MunicipalityId: deben ser mayores que 0
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("Debe especificar un usuario válido.");

        RuleFor(x => x.MunicipalityId)
            .GreaterThan(0).WithMessage("Debe especificar un municipio válido.");

        // CuisineType: debe ser un valor válido del enum
        RuleFor(x => x.CuisineType)
            .IsInEnum().WithMessage("El tipo de cocina no es válido.");
    }

    private bool BeValidImageFile(IFormFile file)
    {
        if (file == null) return false;

        // Verificar tamaño: máximo 5 MB
        if (file.Length > MaxFileSize || file.Length == 0)
            return false;

        // Verificar tipo MIME
        var contentType = file.ContentType.ToLowerInvariant();
        if (!ValidImageTypes.Contains(contentType))
            return false;

        // Opcional: verificar extensión del archivo
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (extension is not ".jpg" and not ".jpeg" and not ".png")
            return false;

        // Opcional: validar magic numbers (cabeceras binarias)
        using var stream = file.OpenReadStream();
        return IsSupportedImage(stream, extension);
    }

    private bool IsSupportedImage(Stream stream, string extension)
    {
        if (stream.Length < 4) return false;

        var buffer = new byte[4];
        stream.Read(buffer, 0, 4);
        stream.Position = 0; // Resetear posición

        // PNG: 89 50 4E 47
        if (extension is ".png" &&
            buffer[0] == 0x89 && buffer[1] == 0x50 && buffer[2] == 0x4E && buffer[3] == 0x47)
            return true;

        // JPG/JPEG: FF D8 FF
        if (extension is ".jpg" or ".jpeg" &&
            buffer[0] == 0xFF && buffer[1] == 0xD8 && buffer[2] == 0xFF)
            return true;

        return false;
    }
}