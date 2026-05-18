using MediatR;

namespace NailSalon.Application.Features.Invoices.Commands.Payment;

public class PayInvoiceCommand : IRequest<Unit>
{
    public Guid InvoiceId { get; set; }
}