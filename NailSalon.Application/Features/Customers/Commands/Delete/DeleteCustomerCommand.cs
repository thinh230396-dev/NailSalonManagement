using MediatR;

namespace NailSalon.Application.Features.Customers.Commands.Delete;

public class DeleteCustomerCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public DeleteCustomerCommand(Guid id)
    {
        Id = id;
    }
}