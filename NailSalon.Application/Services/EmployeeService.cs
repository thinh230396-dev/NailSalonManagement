using AutoMapper;
using NailSalon.Application.DTOs.Employee;
using NailSalon.Application.Interfaces.Services;
using NailSalon.Domain.Entities;
using NailSalon.Application.Interfaces.Repositories;

namespace NailSalon.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
    {
        var employees = await _unitOfWork.Repository<Employee>().GetAllAsync();
        return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
    }

    public async Task<EmployeeDto?> GetEmployeeByIdAsync(Guid id)
    {
        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(id);
        return _mapper.Map<EmployeeDto>(employee);
    }

    public async Task<EmployeeDto> CreateEmployeeAsync(CreateUpdateEmployeeDto dto)
    {
        var employee = _mapper.Map<Employee>(dto);

        await _unitOfWork.Repository<Employee>().AddAsync(employee);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<EmployeeDto>(employee);
    }

    public async Task UpdateEmployeeAsync(Guid id, CreateUpdateEmployeeDto dto)
    {
        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(id);
        if (employee == null) throw new Exception("Không tìm thấy nhân viên.");

        _mapper.Map(dto, employee);

        _unitOfWork.Repository<Employee>().Update(employee);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteEmployeeAsync(Guid id)
    {
        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(id);
        if (employee == null) throw new Exception("Không tìm thấy nhân viên.");

        _unitOfWork.Repository<Employee>().Delete(employee);
        await _unitOfWork.SaveChangesAsync();
    }
}