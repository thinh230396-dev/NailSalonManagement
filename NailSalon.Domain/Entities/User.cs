using NailSalon.Domain.Common;

namespace NailSalon.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public bool IsActive { get; private set; } = true;

    public DateTime? LastLoginAt { get; private set; }

    public Guid RoleId { get; private set; }

    public virtual Role Role { get; private set; } = null!;

    public Guid? EmployeeId { get; private set; }

    public virtual Employee? Employee { get; private set; }

    public virtual ICollection<UserSession> Sessions { get; private set; } = new List<UserSession>();

    public virtual ICollection<AuditLog> AuditLogs { get; private set; } = new List<AuditLog>();

    private User()
    {
    }

    public User(
        string username,
        string passwordHash,
        Guid roleId,
        Guid? employeeId = null)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username không được để trống.");

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("PasswordHash không được để trống.");

        if (roleId == Guid.Empty)
            throw new ArgumentException("RoleId không hợp lệ.");

        Username = username;
        PasswordHash = passwordHash;
        RoleId = roleId;
        EmployeeId = employeeId;
        IsActive = true;
    }

    public void ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentException("PasswordHash mới không được để trống.");

        PasswordHash = newPasswordHash;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void MarkLoginSuccess()
    {
        LastLoginAt = DateTime.UtcNow;
    }

    public void AssignRole(Guid roleId)
    {
        if (roleId == Guid.Empty)
            throw new ArgumentException("RoleId không hợp lệ.");

        RoleId = roleId;
    }

    public void LinkEmployee(Guid employeeId)
    {
        if (employeeId == Guid.Empty)
            throw new ArgumentException("EmployeeId không hợp lệ.");

        EmployeeId = employeeId;
    }

    public void UnlinkEmployee()
    {
        EmployeeId = null;
    }
}