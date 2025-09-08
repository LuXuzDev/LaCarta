using FluentValidation;
using LaCartaAPI.Endpoints.Restaurants.Requests;

public class UpdateRestaurantRequestValidator : AbstractValidator<UpdateRestaurantRequest>
{
    private const int MaxFileSize = 5 * 1024 * 1024; // 5 MB
    private static readonly string[] ValidImageTypes = new[] { "image/jpeg", "image/jpg", "image/png" };

    public UpdateRestaurantRequestValidator()
    {
        // Nombre: si se envía, validar longitud
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del restaurante es obligatorio.")
            .Length(1, 100).WithMessage("El nombre debe tener entre 1 y 100 caracteres.")
            .When(x => x.Name != null);

        // Email: si se envía, validar formato y longitud
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
            .EmailAddress().WithMessage("El correo electrónico no tiene un formato válido.")
            .MaximumLength(256).WithMessage("El correo electrónico no debe exceder los 256 caracteres.")
            .When(x => x.Email != null);

        // Teléfono: si se envía, validar formato
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("El número de teléfono es obligatorio.")
            .Matches(@"^\d{8}$").WithMessage("El número de teléfono debe ser numérico y tener exactamente 8 dígitos.")
            .When(x => x.PhoneNumber != null);

        // Imagen: si se envía, validar archivo
        RuleFor(x => x.Image)
            .Must(BeValidImageFile)
            .WithMessage("La imagen debe ser un archivo JPG o PNG válido y no debe exceder los 5 MB.")
            .When(x => x.Image != null);

        // Horario: si ambos se envían, validar relación
        RuleFor(x => x.CloseHour)
            .GreaterThan(x => x.OpenHour)
            .WithMessage("La hora de cierre debe ser posterior a la hora de apertura.")
            .When(x => x.OpenHour != null && x.CloseHour != null);


        RuleFor(x => x.MunicipalityId)
            .GreaterThan(0).WithMessage("Debe especificar un municipio válido.")
            .When(x => x.MunicipalityId != null);

        // CuisineType: si se envía, debe ser válido
        RuleFor(x => x.CuisineType)
            .IsInEnum().WithMessage("El tipo de cocina no es válido.")
            .When(x => x.CuisineType != null);
    }

    private bool BeValidImageFile(IFormFile file)
    {
        if (file == null) return false;
        if (file.Length > MaxFileSize || file.Length == 0)
            return false;
        var contentType = file.ContentType.ToLowerInvariant();
        if (!ValidImageTypes.Contains(contentType))
            return false;
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (extension is not ".jpg" and not ".jpeg" and not ".png")
            return false;
        using var stream = file.OpenReadStream();
        return IsSupportedImage(stream, extension);
    }

    private bool IsSupportedImage(Stream stream, string extension)
    {
        if (stream.Length < 4) return false;
        var buffer = new byte[4];
        stream.Read(buffer, 0, 4);
        stream.Position = 0;
        if (extension is ".png" &&
            buffer[0] == 0x89 && buffer[1] == 0x50 && buffer[2] == 0x4E && buffer[3] == 0x47)
            return true;
        if (extension is ".jpg" or ".jpeg" &&
            buffer[0] == 0xFF && buffer[1] == 0xD8 && buffer[2] == 0xFF)
            return true;
        return false;
    }
}
