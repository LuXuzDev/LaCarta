namespace Domain.Modules.Shared.Exceptions;

public class EntityNotFoundException : DomainException
{
    public string EntityType { get; }
    public object Identifier { get; }

    public EntityNotFoundException(string entityType, object identifier)
        : base($"No se encontró {entityType} con identificador '{identifier}'.")
    {
        EntityType = entityType;
        Identifier = identifier;
    }
}