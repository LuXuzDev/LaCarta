using Microsoft.AspNetCore.Http;

namespace Domain.FileStorage;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(IFormFile file);
}
