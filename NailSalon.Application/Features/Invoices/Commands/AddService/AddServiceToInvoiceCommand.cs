using MediatR;

namespace NailSalon.Application.Features.Invoices.Commands.AddService;

public class AddServiceToInvoiceCommand : IRequest<Unit>
{
    public Guid InvoiceId { get; set; }

    public Guid NailServiceId { get; set; }

    public int Quantity { get; set; } = 1;
}