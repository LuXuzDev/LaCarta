using Domain.Modules.Restaurants.Enums;
using Domain.Modules.Restaurants.Exceptions;
using Domain.Modules.Restaurants.Interfaces;
using Domain.Modules.Restaurants.Models;
using Domain.Modules.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly AppDbContext _db;


    public RestaurantRepository(AppDbContext db) { _db = db; }


    public async Task ActivateAsync(int restaurantId)
    {
        try
        {
            var restaurant = await _db.Restaurants.FirstOrDefaultAsync
            (r => r.Id == restaurantId);
            if (restaurant == null)
                throw new EntityNotFoundException("Restaurante", restaurantId);
            else
                restaurant.IsActive = true;

            await _db.SaveChangesAsync();
        }
        catch
        {
            throw;
        }
    }


    public async Task DeactivateAsync(int restaurantId)
    {
        try
        {
            var restaurant = await _db.Restaurants.FirstOrDefaultAsync
            (r => r.Id == restaurantId);
            if (restaurant == null)
                throw new EntityNotFoundException("Restaurante", restaurantId);
            else
                restaurant.IsActive = false;

            await _db.SaveChangesAsync();
        }
        catch
        {
            throw;
        }
    }


    public async Task<IEnumerable<Restaurant>> GetAllAsync()
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


    public async Task<Restaurant?> GetByEmailAsync(string email)
    {
        try
        {
            var restaurant = await _db.Restaurants
                .Include(r => r.Municipality)
                .Include(r => r.User)
                .Include(r => r.Dishes)
                .FirstOrDefaultAsync
                        (r => r.Email == email);
            if (restaurant == null)
                throw new EntityNotFoundException("Restaurante", email);
            return restaurant;
        }
        catch
        {
            throw;
        }
    }


    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        try
        {
            var restaurant = await _db.Restaurants
                .Include(r => r.Municipality)
                .Include(r => r.User)
                .Include(r => r.Dishes)
                .FirstOrDefaultAsync
                (r => r.Id == id);
            if (restaurant == null)
                throw new EntityNotFoundException("Restaurante", id);
            return restaurant;
        }
        catch
        {
            throw;
        }
    }


    public async Task AddAsync(Restaurant restaurant)
    {
        try
        {
            var userExist = _db.Users
                .Where(u => u.Id == restaurant.UserId && u.Role != null && u.Role.Name == "RestaurantManager")
                .Any();
            if (!userExist)
                throw new EntityNotFoundException("User", restaurant.UserId);

            var nameIsUnique = !_db.Restaurants.Any(r => r.Name == restaurant.Name);
            if (!nameIsUnique)
                throw new NotUniqueNameException(restaurant.Name);

            var emailIsUnique = !_db.Restaurants.Any(r => r.Email == restaurant.Email);
            if (!emailIsUnique)
                throw new NotUniqueEmailException(restaurant.Email);

            var phoneNumberIsUnique = !_db.Restaurants.Any(r => r.PhoneNumber == restaurant.PhoneNumber);
            if (!phoneNumberIsUnique)
                throw new NotUniquePhoneNumber(restaurant.PhoneNumber);

            var municipalityExist = _db.Municipality.Any(m => m.Id == restaurant.MunicipalityId);
            if (!municipalityExist)
                throw new EntityNotFoundException("Municipality", restaurant.MunicipalityId);

            await _db.Restaurants.AddAsync(restaurant);
            await _db.SaveChangesAsync();
        }
        catch
        {
            throw;
        }
    }



    public async Task<bool> IsEmailUniqueAsync(string email, int? excludeRestaurantId = null)
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


    public async Task<bool> IsNameUniqueAsync(string name, int? excludeRestaurantId = null)
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


    public async Task UpdateAsync(Restaurant restaurant)
    {
        try
        {
            var existing = await _db.Restaurants.FirstOrDefaultAsync(r => r.Id == restaurant.Id);
            if (existing == null)
                throw new EntityNotFoundException("Restaurante", restaurant.Id);

            // Actualiza los campos necesarios
            existing.Name = restaurant.Name;
            existing.Email = restaurant.Email;
            existing.PhoneNumber = restaurant.PhoneNumber;
            existing.Image = restaurant.Image;
            existing.HasDelivery = restaurant.HasDelivery;
            existing.OpenHour = restaurant.OpenHour;
            existing.CloseHour = restaurant.CloseHour;
            existing.UserId = restaurant.UserId;
            existing.MunicipalityId = restaurant.MunicipalityId;
            existing.IsActive = restaurant.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
        }
        catch
        {
            throw;
        }
    }


    public async Task<IEnumerable<Restaurant>> GetByRestaurantsManagerId(int id)
    {
        return await _db.Restaurants.Where(r => r.UserId == id)
            .Include(r => r.Municipality)
            .Include(r => r.User)
            .Include(r => r.Dishes)
            .ToListAsync();
    }


    public async Task<IEnumerable<Restaurant>> GetRestaurantsActiveAsync()
    {
        return await _db.Restaurants.Where(r => r.IsActive)
           .Include(r => r.Municipality)
          .Include(r => r.User)
          .Include(r => r.Dishes)
          .ToListAsync();
    }


    public async Task<IEnumerable<Restaurant>> GetByRestaurantsCousineType(CuisineType cousineTypeId)
    {
        return await _db.Restaurants.Where(r => r.CuisineType == cousineTypeId )
            .Include(r => r.Municipality)
          .Include(r => r.User)
          .Include(r => r.Dishes)
          .ToListAsync();
    }
}
