using Domain.FileStorage;
using Microsoft.AspNetCore.Http;

namespace Data.FileStorage;

public class LocalFileStorageService : IFileStorageService
{
    public async Task<string> SaveFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("El archivo no es válido.", nameof(file));

        // Carpeta donde se guardarán las imágenes
        var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images");

        // Crear la carpeta si no existe
        if (!Directory.Exists(imagesFolder))
            Directory.CreateDirectory(imagesFolder);

        // Generar nombre único para el archivo
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(imagesFolder, fileName);

        // Guardar el archivo
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Retornar la ruta relativa
        return Path.Combine("Images", fileName);
    }
}
