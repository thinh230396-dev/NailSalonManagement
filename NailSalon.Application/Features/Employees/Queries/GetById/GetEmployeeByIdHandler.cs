using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Features.Employees.DTOs;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Employees.Queries.GetById;

public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetEmployeeByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.Id);

        if (employee == null)
            throw new NotFoundException(nameof(Employee), request.Id);

        return new EmployeeDto
        {
            Id = employee.Id,
            FullName = employee.FullName,
            PhoneNumber = employee.PhoneNumber,
            Position = employee.Position
        };
    }
}