using MediatR;
using NailSalon.Application.Features.Employees.DTOs;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Employees.Queries.GetList;

public class GetEmployeeListHandler : IRequestHandler<GetEmployeeListQuery, List<EmployeeDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetEmployeeListHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<EmployeeDto>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
    {
        var employees = await _unitOfWork.Repository<Employee>().GetAllAsync();

        return employees.Select(x => new EmployeeDto
        {
            Id = x.Id,
            FullName = x.FullName,
            PhoneNumber = x.PhoneNumber,
            Position = x.Position
        }).ToList();
    }
}