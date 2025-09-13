using Domain.Modules.Dishs.Enums;
using Microsoft.AspNetCore.Http;

namespace Business.Modules.Dishes.DTOs;

public class CreateDishDTO
{
    public required string Name { get; set; }
    public IFormFile Image { get; set; }
    public decimal Price { get; set; }
    public required string Description { get; set; }
    public required DishType DishType { get; set; }
}

