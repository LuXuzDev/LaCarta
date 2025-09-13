using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LaCartaAPI.Endpoints.Dishes.Request;
    public class GetDishesRequest
    {
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del restaurante debe ser un número positivo.")]
    public int RestaurantId { get; set; }
}