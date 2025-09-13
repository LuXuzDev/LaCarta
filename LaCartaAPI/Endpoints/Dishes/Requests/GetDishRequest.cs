using System.ComponentModel.DataAnnotations;

namespace LaCartaAPI.Endpoints.Dishes.Requests;

public class GetDishRequest
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del plato debe ser un número entero positivo.")]
    public int DishId { get; set; }
}