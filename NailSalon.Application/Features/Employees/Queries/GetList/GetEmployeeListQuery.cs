using MediatR;
using NailSalon.Application.Features.Employees.DTOs;

namespace NailSalon.Application.Features.Employees.Queries.GetList;

public class GetEmployeeListQuery : IRequest<List<EmployeeDto>>
{
}