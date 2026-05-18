using MediatR;

namespace NailSalon.Application.Features.Invoices.Commands.Update;

public class UpdateInvoiceCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public string InvoiceCode { get; set; } = string.Empty;

    public Guid CustomerId { get; set; }

    public Guid EmployeeId { get; set; }
}