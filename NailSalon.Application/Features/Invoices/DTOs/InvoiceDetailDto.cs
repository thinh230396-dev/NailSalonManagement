namespace NailSalon.Application.Features.Invoices.DTOs;

public class InvoiceDetailDto
{
    public Guid Id { get; set; }

    public Guid InvoiceId { get; set; }

    public Guid NailServiceId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal LineTotal => Quantity * UnitPrice;
}