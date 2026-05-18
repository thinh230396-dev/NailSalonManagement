using MediatR;
using NailSalon.Application.Features.Customers.DTOs;

namespace NailSalon.Application.Features.Customers.Queries.GetById;

public class GetCustomerByIdQuery : IRequest<CustomerDto>
{
    public Guid Id { get; set; }

    public GetCustomerByIdQuery(Guid id)
    {
        Id = id;
    }
}