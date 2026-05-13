using NailSalon.Domain.Common;
using NailSalon.Domain.Enums;

namespace NailSalon.Domain.Entities;

public class Role : BaseEntity
{
    public RoleType RoleType { get; set; }
    public string RoleName { get; set; } = string.Empty;

    // Navigation Property
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}