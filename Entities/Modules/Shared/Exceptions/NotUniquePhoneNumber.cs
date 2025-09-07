namespace Domain.Modules.Shared.Exceptions;

public class NotUniquePhoneNumber : DomainException
{
    public string PhoneNumber { get; }

    public NotUniquePhoneNumber(string name)
        : base($"El nombre '{name}' ya esta en uso.")
    {
        PhoneNumber = name;
    }
}
