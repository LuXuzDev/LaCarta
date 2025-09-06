using Domain.Modules.Dishs.Enums;
using Domain.Modules.Shared.Entities;

namespace Domain.Modules.Dishs.Models;

public class DishWarning : BaseEntity
{
    public int DishId { get; set; }
    public Dish? Dish { get; set; }

    public WarningType WarningType { get; set; }
}
