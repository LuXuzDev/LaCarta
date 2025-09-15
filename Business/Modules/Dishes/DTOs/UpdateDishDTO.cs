using Microsoft.AspNetCore.Http;

namespace Business.Modules.Dishes.DTOs;

public class UpdateDishDTO
{
    public required int Id { get; set; }
    public string? Name { get; set; }
    public IFormFile? Image { get; set; }
    public decimal? Price { get; set; }
    public string? Description { get; set; }
    public bool? IsActive { get; set; }

}
