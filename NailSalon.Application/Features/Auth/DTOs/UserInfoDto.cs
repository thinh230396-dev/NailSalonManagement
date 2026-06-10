namespace NailSalon.Application.Features.Auth.DTOs;

public class UserInfoDto
{
    public Guid Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string RoleName { get; set; } = string.Empty;

    public Guid? EmployeeId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LastLoginAt { get; set; }
}