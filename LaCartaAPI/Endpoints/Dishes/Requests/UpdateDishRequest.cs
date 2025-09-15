using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCartaAPI.Endpoints.Dishes.Request;

public class UpdateDishRequest
{
    public required int Id { get; set; }
    public string? Name { get; set; }
    public IFormFile? Image { get; set; }
    public decimal? Price { get; set; }
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
}