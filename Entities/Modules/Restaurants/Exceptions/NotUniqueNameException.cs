using Domain.Modules.Shared.Exceptions;

namespace Domain.Modules.Restaurants.Exceptions;

public class NotUniqueNameException : DomainException
{
    public string Name { get; }

    public NotUniqueNameException(string name)
        : base($"El nombre '{name}' ya esta en uso.")
    {
        Name = name;
    }
}
