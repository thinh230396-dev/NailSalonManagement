namespace NailSalon.Application.Features.Auth.DTOs;

public class RegisterDto
{
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public Guid RoleId { get; set; }

    public Guid? EmployeeId { get; set; }
}