using NailSalon.Domain.Common;

namespace NailSalon.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; } = null!;

    // Liên kết tài khoản này với một nhân viên cụ thể (nếu có)
    public Guid? EmployeeId { get; set; }
    public virtual Employee? Employee { get; set; }
}