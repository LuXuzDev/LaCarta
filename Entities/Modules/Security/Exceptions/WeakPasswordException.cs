using Domain.Modules.Shared.Exceptions;


namespace Domain.Modules.Security.Exceptions;

public class WeakPasswordException : DomainException
{
    public WeakPasswordException(string reason)
    : base($"La contraseña no cumple con los requisitos de seguridad: {reason}. Por favor, elige una contraseña más segura.")
    {
    }
}
