using Domain.Modules.Shared.Entities;

namespace Domain.Modules.Users.Models;

public class Role : BaseEntity
{
    public string Name { get; set; }

    public ICollection<User> Users;
}
