using System.Runtime.Serialization;

namespace Domain.Modules.Shared.Exceptions;
/// <summary>
/// Excepción base para errores del dominio de negocio.
/// </summary>
[Serializable]
public abstract class DomainException : Exception
{
    protected DomainException() { }

    protected DomainException(string message)
        : base(message) { }

    protected DomainException(string message, Exception innerException)
        : base(message, innerException) { }

    protected DomainException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}