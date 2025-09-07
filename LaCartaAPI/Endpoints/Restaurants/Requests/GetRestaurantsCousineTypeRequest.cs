using Domain.Modules.Restaurants.Enums;

namespace LaCartaAPI.Endpoints.Restaurants.Requests;

public class GetRestaurantsCousineTypeRequest
{
    public CuisineType cousineType { get; set; }
}
