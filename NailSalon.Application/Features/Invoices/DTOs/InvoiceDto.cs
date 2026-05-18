namespace NailSalon.Application.Features.Invoices.DTOs;

public class InvoiceDto
{
    public Guid Id { get; set; }

    public string InvoiceCode { get; set; } = string.Empty;

    public DateTime PaymentDate { get; set; }

    public decimal TotalAmount { get; set; }

    public Guid CustomerId { get; set; }

    public Guid EmployeeId { get; set; }
}