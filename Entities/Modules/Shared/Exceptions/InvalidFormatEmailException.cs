namespace Domain.Modules.Shared.Exceptions;

public class InvalidFormatEmailException : DomainException
{
    public string Email { get; }

    public InvalidFormatEmailException(string email)
        : base($"El correo '{email}' no tiene un formato válido.")
    {
        Email = email;
    }
}
