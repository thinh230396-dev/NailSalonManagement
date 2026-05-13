using AutoMapper;
using NailSalon.Application.DTOs.Employee;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Mappings;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeDto>();
        CreateMap<CreateUpdateEmployeeDto, Employee>();
    }
}