using Domain.Modules.Address;
using Domain.Modules.Address.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Data.Repositories;

public class MunicipalityRepository : IMunicipalityRepository
{
    private readonly AppDbContext _db;
    
    public MunicipalityRepository(AppDbContext db)
    {
        _db = db;
    }


    public async Task<Municipality?> GetByIdAsync(int municipalityId, CancellationToken ct)
    {
        var municipality = await _db.Municipality.FirstOrDefaultAsync(m => m.Id == municipalityId);
        return municipality;
    }
}
