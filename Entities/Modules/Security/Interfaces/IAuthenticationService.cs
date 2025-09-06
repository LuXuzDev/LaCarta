using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modules.Security.Interfaces;

public interface IAuthenticationService
{
    Task<AuthenticationResult> LoginAsync(string email, string password);
    Task LogoutAsync(string tokenOrSessionId);
    Task<bool> IsTokenValidAsync(string token);
    Task<bool> IsSessionActiveAsync(string sessionId);
}

// Resultado de autenticación
public record AuthenticationResult(
    bool Success,
    string? Token,
    string? RefreshToken,
    DateTime? ExpiresAt,
    IEnumerable<string> Roles,
    string? ErrorMessage
);
