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
        try
        {
            var restaurants = await _db.Restaurants
                .Include(r => r.Municipality)
                .Include(r => r.User)
                .Include(r => r.Dishes)
                .ToListAsync();
            return restaurants;
        }
        catch
        {
            throw;
        }
    }


    public async Task<Restaurant?> GetByEmailAsync(string email, CancellationToken ct)
    {
        try
        {
            var restaurant = await _db.Restaurants
                .Include(r => r.Municipality)
                .Include(r => r.User)
                .Include(r => r.Dishes)
                .FirstOrDefaultAsync(r => r.Email == email);
            return restaurant;
        }
        catch
        {
            throw;
        }
    }


    public async Task<Restaurant?> GetByIdAsync(int restaurantId, CancellationToken ct)
    {
        try
        {
            var restaurant = await _db.Restaurants
                .Include(r => r.Municipality)
                .Include(r => r.User)
                .Include(r => r.Dishes)
                .FirstOrDefaultAsync
                (r => r.Id == restaurantId);
            return restaurant;
        }
        catch
        {
            throw;
        }
    }


    public async Task<IEnumerable<Restaurant>> GetByRestaurantsManagerId(int managerId, CancellationToken ct)
    {
        return await _db.Restaurants.Where(r => r.UserId == managerId)
            .Include(r => r.Municipality)
            .Include(r => r.User)
            .Include(r => r.Dishes)
            .ToListAsync();
    }


    public async Task<IEnumerable<Restaurant>> GetRestaurantsActiveAsync(CancellationToken ct)
    {
        return await _db.Restaurants.Where(r => r.IsActive)
           .Include(r => r.Municipality)
          .Include(r => r.User)
          .Include(r => r.Dishes)
          .ToListAsync();
    }


    public async Task<IEnumerable<Restaurant>> GetByRestaurantsCousineType(CuisineType cousineTypeId, CancellationToken ct)
    {
        return await _db.Restaurants.Where(r => r.CuisineType == cousineTypeId)
            .Include(r => r.Municipality)
          .Include(r => r.User)
          .Include(r => r.Dishes)
          .ToListAsync();
    }
    #endregion

    public async Task AddAsync(Restaurant restaurant, CancellationToken ct)
    {
        try
        {
            await _db.Restaurants.AddAsync(restaurant);
            await _db.SaveChangesAsync();
        }
        catch
        {
            throw;
        }
    }


    public async Task UpdateAsync(Restaurant restaurant, CancellationToken ct)
    {
        try
        {
            // Actualiza los campos necesarios
            restaurant.Name = restaurant.Name;
            restaurant.Email = restaurant.Email;
            restaurant.PhoneNumber = restaurant.PhoneNumber;
            restaurant.Image = restaurant.Image;
            restaurant.HasDelivery = restaurant.HasDelivery;
            restaurant.OpenHour = restaurant.OpenHour;
            restaurant.CloseHour = restaurant.CloseHour;
            restaurant.UserId = restaurant.UserId;
            restaurant.MunicipalityId = restaurant.MunicipalityId;
            restaurant.IsActive = restaurant.IsActive;
            restaurant.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
        }
        catch
        {
            throw;
        }
    }


    #region Validations
    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken ct, int? excludeRestaurantId = null)
    {
        try
        {
            return !await _db.Restaurants
                .AnyAsync(r => r.Email == email && (!excludeRestaurantId.HasValue || r.Id != excludeRestaurantId.Value));
        }
        catch
        {
            throw;
        }
    }


    public async Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber, CancellationToken ct, int? excludeRestaurantId = null)
    {
        try
        {
            return !await _db.Restaurants
                .AnyAsync(r => r.PhoneNumber == phoneNumber && (!excludeRestaurantId.HasValue || r.Id != excludeRestaurantId.Value));
        }
        catch
        {
            throw;
        }
    }


    public async Task<bool> IsNameUniqueAsync(string name, CancellationToken ct, int? excludeRestaurantId = null)
    {
        try
        {
            return !await _db.Restaurants
                .AnyAsync(r => r.Name == name && (!excludeRestaurantId.HasValue || r.Id != excludeRestaurantId.Value));
        }
        catch
        {
            throw;
        }
    }
    #endregion
}
