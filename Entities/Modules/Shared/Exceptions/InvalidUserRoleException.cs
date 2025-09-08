namespace Domain.Modules.Shared.Exceptions;


public class InvalidUserRoleException : DomainException
{
    public InvalidUserRoleException(string message)
        : base(message)
    {
    }
}