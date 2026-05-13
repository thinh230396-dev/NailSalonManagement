using NailSalon.Application.DTOs.Customer;

namespace NailSalon.Application.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
    Task<CustomerDto?> GetCustomerByIdAsync(Guid id);

    // Dùng chung CreateUpdateCustomerDto
    Task<CustomerDto> CreateCustomerAsync(CreateUpdateCustomerDto dto);
    Task UpdateCustomerAsync(Guid id, CreateUpdateCustomerDto dto);

    Task DeleteCustomerAsync(Guid id);
}