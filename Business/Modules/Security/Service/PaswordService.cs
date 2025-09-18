using System.Text;
using System.Security.Cryptography;
using Domain.Modules.Security.Interfaces;

namespace Business.Modules.Security.Service;

public class PasswordService : IPasswordService
{
    public string Hash(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        var hashString = Convert.ToBase64String(hash);
        return hashString;
    }

    public bool Verify(string hashedPassword, string providedPassword)
    {
        // Comparamos el hash del password proporcionado con el hash almacenado
        return Hash(providedPassword).Equals(hashedPassword);

    }
}
