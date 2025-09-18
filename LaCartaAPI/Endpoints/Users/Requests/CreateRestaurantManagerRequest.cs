namespace LaCartaAPI.Endpoints.Users.Requests;

public class CreateRestaurantManagerRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
