using Domain.Modules.Dishs.Enums;
using Domain.Modules.Shared.Entities;


namespace Domain.Modules.Dishs.Models;

public class Dish : BaseEntity
{
    public required string Name { get; set; }
    public string? Image { get; set; }
    public decimal Price { get; set; }
    public required string Description { get; set; }

    public required DishType DishType { get; set; }
}
