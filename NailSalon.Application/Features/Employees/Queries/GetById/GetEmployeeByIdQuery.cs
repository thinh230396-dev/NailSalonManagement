using MediatR;
using NailSalon.Application.Features.Employees.DTOs;

namespace NailSalon.Application.Features.Employees.Queries.GetById;

public class GetEmployeeByIdQuery : IRequest<EmployeeDto>
{
    public Guid Id { get; set; }

    public GetEmployeeByIdQuery(Guid id)
    {
        Id = id;
    }
}