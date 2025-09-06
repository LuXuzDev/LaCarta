namespace Business.Modules.Restaurants.DTOs;

public record RestaurantDTO
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public string? Image { get; set; }
    public bool HasDelivery { get; set; }
    public TimeSpan OpenHour { get; set; }
    public TimeSpan CloseHour { get; set; }

    public int UserId { get; set; }
    public string UserEmail { get; set; }

    public int MunicipalityId { get; set; }
    public string MunicipalityName { get; set; }

    public string CuisineType { get; set; }

    //public ICollection<DishDTO> Dishes { get; set; } = new List<DishDTO>();
}
