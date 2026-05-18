namespace NailSalon.Application.Features.Invoices.DTOs;

public class UpdateInvoiceDto
{
    public Guid Id { get; set; }

    public string InvoiceCode { get; set; } = string.Empty;

    public Guid CustomerId { get; set; }

    public Guid EmployeeId { get; set; }
}