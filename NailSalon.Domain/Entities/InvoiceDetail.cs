using NailSalon.Domain.Common;

namespace NailSalon.Domain.Entities;

public class InvoiceDetail : BaseEntity
{
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; } // Lưu lại giá tại thời điểm thanh toán để tránh sai số khi NailService đổi giá

    // Foreign Keys
    public Guid InvoiceId { get; set; }
    public virtual Invoice Invoice { get; set; } = null!;

    public Guid NailServiceId { get; set; }
    public virtual NailService NailService { get; set; } = null!;
}