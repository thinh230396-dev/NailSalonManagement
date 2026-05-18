using MediatR;

namespace NailSalon.Application.Features.Invoices.Commands.Delete;

public class DeleteInvoiceCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public DeleteInvoiceCommand(Guid id)
    {
        Id = id;
    }
}