namespace Domain.Modules.Address.Interfaces;

public interface IMunicipalityRepository
{
    // Consultas
    Task<Municipality?> GetByIdAsync (int municipalityId, CancellationToken ct);
}
