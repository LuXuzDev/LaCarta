using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Domain.Modules.Dishs.Interfaces;
using Domain.Modules.Dishs.Models;
using Domain.Modules.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class DishRepository : IDishRepository
{
    private readonly AppDbContext _db;

    public DishRepository(AppDbContext db) { _db = db; }


    public async Task AddAsync(Dish dish, CancellationToken ct)
    {
        try
        {
            await _db.Dishs.AddAsync(dish);
            await _db.SaveChangesAsync();
        }
        catch
        {
            throw;
        }
    }

    public async Task<Dish> GetByIdAsync(int dishId, CancellationToken ct)
    {
        var dish = await _db.Dishs
            .FirstOrDefaultAsync(d => d.Id == dishId, ct);

        if (dish == null)
            throw new EntityNotFoundException("Dish", dishId);

        return dish;
    }

    public async Task<IEnumerable<Dish>> GetByRestaurantIdAsync(int restaurantId, CancellationToken ct)
    {
        return await _db.Dishs
        .Where(d => d.RestaurantId == restaurantId)
        .ToListAsync(ct);

    }
    public async Task<bool> IsNameUniqueAsync(string name, int restaurantId, int? excludeDishId = null)
    {
        try
        {
            return !await _db.Dishs
           .AnyAsync(d => d.Name == name
            && d.RestaurantId == restaurantId
            && (!excludeDishId.HasValue || d.Id != excludeDishId.Value));
        }
        catch
        {
            throw;
        }
    }

    public Task ToggleStateAsync(int id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }


    public async Task UpdateAsync(CancellationToken ct)
    {
        try
        {
            await _db.SaveChangesAsync(ct);
        }
        catch
        {
            throw;
        }
    }
}
