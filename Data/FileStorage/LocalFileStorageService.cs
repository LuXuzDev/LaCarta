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

    public async Task<bool> DeleteFileAsync(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            return false;

        try
        {
            // Convertir ruta relativa a física
            var physicalPath = Path.Combine(Directory.GetCurrentDirectory(), filePath.Replace("/", "\\"));

            // Verificar que el archivo esté dentro de la carpeta permitida (evitar ../)
            var rootPath = Path.GetFullPath(Directory.GetCurrentDirectory());
            var fileFullPath = Path.GetFullPath(physicalPath);

            if (!fileFullPath.StartsWith(rootPath, StringComparison.OrdinalIgnoreCase))
                return false; // Posible ataque de navegación de directorios

            if (File.Exists(physicalPath))
            {
                File.Delete(physicalPath);
                return true;
            }

            return false; // No existe, pero no es un error
        }
        catch (IOException)
        {
            // No se puede acceder (en uso, permisos, etc.)
            return false;
        }
        catch (UnauthorizedAccessException)
        {
            return false;
        }
    }

    public Task<bool> FileExistsAsync(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            return Task.FromResult(false);

        var physicalPath = Path.Combine(Directory.GetCurrentDirectory(), filePath.Replace("/", "\\"));
        return Task.FromResult(File.Exists(physicalPath));
    }
}
