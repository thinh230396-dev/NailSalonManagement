using MediatR;

namespace NailSalon.Application.Features.Invoices.Commands.Create;

public class CreateInvoiceCommand : IRequest<Guid>
{
    public string InvoiceCode { get; set; } = string.Empty;

    public Guid CustomerId { get; set; }

    public Guid EmployeeId { get; set; }
}