namespace Domain.Modules.Shared.Exceptions;

public class InvalidTimeRangeException : DomainException
{
    public InvalidTimeRangeException(string message)
        : base(message)
    {
    }
}