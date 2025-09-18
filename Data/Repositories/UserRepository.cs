using Domain.Modules.Users.Interfaces;
using Domain.Modules.Users.Models;
using Microsoft.EntityFrameworkCore;


namespace Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;


    public UserRepository(AppDbContext db) { _db = db; }


    #region Getters
    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken ct)
    {
        var user = await _db.Users
        .Include(u => u.Role)
        .Include(u => u.Favorites)
        .ToListAsync(ct);
        return user;
    }


    public async Task<User?> GetByIdAsync(int userId, CancellationToken ct)
    {
        var user = await _db.Users
           .Include(u => u.Role)
           .Include(u => u.Favorites)
           .FirstOrDefaultAsync(r => r.Id == userId, ct);
        return user;
    }
    #endregion


    #region Commands
    public async Task ActivateAsync(User user, CancellationToken ct)
    {
        user.IsActive = true;
        _db.Users.Update(user);
        await _db.SaveChangesAsync(ct);
    }


    public async Task AddAsync(User user, CancellationToken ct)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync(ct);
    }


    public async Task DeactivateAsync(User user, CancellationToken ct)
    {
        user.IsActive = false;
        _db.Users.Update(user);
        await _db.SaveChangesAsync(ct);
    }


    public async Task DeleteAsyn(User user, CancellationToken ct)
    {
        _db.Users.Remove(user);
        await _db.SaveChangesAsync(ct);
    }


    public async Task UpdateAsync(User user, CancellationToken ct)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync(ct);
    }
    #endregion


    #region Validations
    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken ct, int? excludeUserId = null)
    {
        return !await _db.Users
            .AnyAsync(u => u.Email == email && (!excludeUserId.HasValue || u.Id != excludeUserId.Value));
    }
    #endregion
}
