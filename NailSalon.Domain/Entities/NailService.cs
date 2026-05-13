using NailSalon.Domain.Common;

namespace NailSalon.Domain.Entities;

public class NailService : BaseEntity
{
    public string ServiceName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int DurationInMinutes { get; set; } // Thời gian thực hiện (phút)

    // Navigation Property
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
}