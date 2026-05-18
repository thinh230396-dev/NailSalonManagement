using MediatR;
using NailSalon.Application.Features.Customers.DTOs;

namespace NailSalon.Application.Features.Customers.Queries.GetList;

public class GetCustomerListQuery : IRequest<List<CustomerDto>>
{
}