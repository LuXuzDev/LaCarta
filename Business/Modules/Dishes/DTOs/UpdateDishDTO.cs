namespace Business.Modules.Dishes.DTOs;

public class UpdateDishDTO
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Image { get; set; }
    public decimal Price { get; set; }
    public required string Description { get; set; }
    public bool IsActive { get; set; }

}
