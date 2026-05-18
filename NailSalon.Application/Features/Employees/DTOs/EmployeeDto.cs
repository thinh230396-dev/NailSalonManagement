namespace NailSalon.Application.Features.Employees.DTOs;

public class EmployeeDto
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Position { get; set; } = string.Empty;
}