using Domain.Modules.Dishs.Enums;

namespace Business.Modules.Dishes.DTOs;

public class DishDTO
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Image { get; set; }
    public decimal Price { get; set; }
    public required string Description { get; set; }
    public required DishType DishType { get; set; }
    public bool IsActive { get; set; }


}
