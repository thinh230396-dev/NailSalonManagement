using NailSalon.Application.DTOs.Employee; // <-- Dòng này cực kỳ quan trọng

namespace NailSalon.Application.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
    Task<EmployeeDto?> GetEmployeeByIdAsync(Guid id);
    Task<EmployeeDto> CreateEmployeeAsync(CreateUpdateEmployeeDto dto);
    Task UpdateEmployeeAsync(Guid id, CreateUpdateEmployeeDto dto);
    Task DeleteEmployeeAsync(Guid id);
}