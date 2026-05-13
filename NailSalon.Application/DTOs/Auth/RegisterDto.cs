namespace NailSalon.Application.DTOs.Auth;

public class RegisterDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Guid EmployeeId { get; set; }
    public Guid RoleId { get; set; }
}