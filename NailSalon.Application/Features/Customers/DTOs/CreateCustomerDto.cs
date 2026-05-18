namespace NailSalon.Application.Features.Customers.DTOs;

public class CreateCustomerDto
{
    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string? Email { get; set; }
}