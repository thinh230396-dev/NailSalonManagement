namespace NailSalon.Application.Features.Invoices.DTOs;

public class CreateInvoiceDto
{
    public string InvoiceCode { get; set; } = string.Empty;

    public Guid CustomerId { get; set; }

    public Guid EmployeeId { get; set; }
}