using Domain.Modules.Dishs.Enums;

namespace LaCartaAPI.Endpoints.Restaurants.Requests;

public class CreateDishRequest
{
    public required string Name { get; set; }
    public IFormFile Image { get; set; }
    public decimal Price { get; set; }
    public required string Description { get; set; }
    public required DishType DishType { get; set; }
    public required int RestaurantId { get; set; }
    }
