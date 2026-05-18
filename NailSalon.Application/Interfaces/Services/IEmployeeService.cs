using NailSalon.Application.DTOs.Employee;

namespace NailSalon.Application.Interfaces.Services;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
    Task<EmployeeDto?> GetEmployeeByIdAsync(Guid id);
    Task<EmployeeDto> CreateEmployeeAsync(CreateUpdateEmployeeDto dto);
    Task UpdateEmployeeAsync(Guid id, CreateUpdateEmployeeDto dto);
    Task DeleteEmployeeAsync(Guid id);
}