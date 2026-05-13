using NailSalon.Domain.Common;

namespace NailSalon.Domain.Entities;

public class Invoice : BaseEntity
{
    public string InvoiceCode { get; set; } = string.Empty;
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }

    // Foreign Keys
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = null!;

    public Guid EmployeeId { get; set; }
    public virtual Employee Employee { get; set; } = null!;

    // Navigation Property
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
}