namespace NailSalon.Application.DTOs.Customer;

public class CreateUpdateCustomerDto
{
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Email { get; set; }
}