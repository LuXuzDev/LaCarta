using Domain.Modules.Users.Interfaces;
using Domain.Modules.Users.Models;
using Microsoft.EntityFrameworkCore;


namespace Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db) { _db = db; }



    public Task AddAsync(User user, CancellationToken ct)
    {
        throw new NotImplementedException();
    }


    public Task<IEnumerable<User>> GetAllAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }


    public Task<User?> GetByEmailAsync(string email, CancellationToken ct)
    {
        throw new NotImplementedException();
    }


    public async Task<User?> GetByIdAsync(int userId, CancellationToken ct)
    {
        try
        {
            var user = await _db.Users
                .Include(u => u.Role)
                .Include(u => u.Favorites)
                .Include(u => u.ManagedRestaurants)
               .FirstOrDefaultAsync(r => r.Id == userId);
            return user;
        }
        catch 
        {
            throw;
        }
    }


    public Task<bool> IsEmailUniqueAsync(string email,CancellationToken ct,int? excludeUserId = null)
    {
        throw new NotImplementedException();
    }


    public Task UpdateAsync(User user, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
