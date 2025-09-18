using Domain.Modules.Restaurants.Enums;
using Domain.Modules.Restaurants.Interfaces;
using Domain.Modules.Restaurants.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly AppDbContext _db;


    public RestaurantRepository(AppDbContext db) { _db = db; }


    #region Getters
    public async Task<IEnumerable<Restaurant>> GetAllAsync(CancellationToken ct)
    {
        var restaurants = await _db.Restaurants
            .Include(r => r.Municipality)
            .Include(r => r.User)
            .Include(r => r.Dishes)
            .ToListAsync(ct);
        return restaurants;
    }


    public async Task<Restaurant?> GetByEmailAsync(string email, CancellationToken ct)
    {
        var restaurant = await _db.Restaurants
            .Include(r => r.Municipality)
            .Include(r => r.User)
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(r => r.Email == email, ct);
        return restaurant;
    }


    public async Task<Restaurant?> GetByIdAsync(int restaurantId, CancellationToken ct)
    {
        var restaurant = await _db.Restaurants
            .Include(r => r.Municipality)
            .Include(r => r.User)
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync
            (r => r.Id == restaurantId,ct);
        return restaurant;
    }


    public async Task<IEnumerable<Restaurant>> GetByRestaurantsManagerId(int managerId, CancellationToken ct)
    {
        return await _db.Restaurants.Where(r => r.UserId == managerId)
            .Include(r => r.Municipality)
            .Include(r => r.User)
            .Include(r => r.Dishes)
            .ToListAsync(ct);
    }


    public async Task<IEnumerable<Restaurant>> GetRestaurantsActiveAsync(CancellationToken ct)
    {
        return await _db.Restaurants.Where(r => r.IsActive)
           .Include(r => r.Municipality)
          .Include(r => r.User)
          .Include(r => r.Dishes)
          .ToListAsync(ct);
    }


    public async Task<IEnumerable<Restaurant>> GetByRestaurantsCousineType(CuisineType cousineTypeId, CancellationToken ct)
    {
        return await _db.Restaurants.Where(r => r.CuisineType == cousineTypeId)
            .Include(r => r.Municipality)
          .Include(r => r.User)
          .Include(r => r.Dishes)
          .ToListAsync(ct);
    }
    #endregion



    #region Commands
    public async Task AddAsync(Restaurant restaurant, CancellationToken ct)
    {
        await _db.Restaurants.AddAsync(restaurant);
        await _db.SaveChangesAsync(ct);
    }


    public async Task UpdateAsync(Restaurant restaurant, CancellationToken ct)
    {
        _db.Restaurants.Update(restaurant);
        await _db.SaveChangesAsync(ct);
    }


    public async Task ActivateAsync(Restaurant restaurant, CancellationToken ct)
    {
        restaurant.IsActive = true;
        _db.Restaurants.Update(restaurant);
        await _db.SaveChangesAsync(ct);
    }


    public async Task DeactivateAsync(Restaurant restaurant, CancellationToken ct)
    {
        restaurant.IsActive = false;
        _db.Restaurants.Update(restaurant);
        await _db.SaveChangesAsync(ct);
    }
    #endregion


    #region Validations
    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken ct, int? excludeRestaurantId = null)
    {

        return !await _db.Restaurants
            .AnyAsync(r => r.Email == email && (!excludeRestaurantId.HasValue || r.Id != excludeRestaurantId.Value));
    }


    public async Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber, CancellationToken ct, int? excludeRestaurantId = null)
    {
        return !await _db.Restaurants
            .AnyAsync(r => r.PhoneNumber == phoneNumber && (!excludeRestaurantId.HasValue || r.Id != excludeRestaurantId.Value));
    }


    public async Task<bool> IsNameUniqueAsync(string name, CancellationToken ct, int? excludeRestaurantId = null)
    {
        return !await _db.Restaurants
            .AnyAsync(r => r.Name == name && (!excludeRestaurantId.HasValue || r.Id != excludeRestaurantId.Value));
    }
    #endregion
}
