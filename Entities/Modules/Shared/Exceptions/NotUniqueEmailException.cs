namespace Domain.Modules.Shared.Exceptions;

public class NotUniqueEmailException : DomainException
{
    public string Email { get; }

    public NotUniqueEmailException(string email)
        : base($"El correo '{email}' ya esta en uso.")
    {
        Email = email;
    }
}
