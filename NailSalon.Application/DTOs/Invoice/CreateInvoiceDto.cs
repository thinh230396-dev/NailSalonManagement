namespace NailSalon.Application.DTOs.Invoice;

public class CreateInvoiceDto
{
    public Guid CustomerId { get; set; }
    public Guid EmployeeId { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; } = "Tiền mặt"; // Ví dụ: Tiền mặt, Chuyển khoản, Thẻ
    public string? Notes { get; set; }
}