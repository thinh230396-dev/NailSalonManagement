namespace NailSalon.Application.Features.Employees.DTOs;

public class CreateEmployeeDto
{
    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Position { get; set; } = string.Empty;
}