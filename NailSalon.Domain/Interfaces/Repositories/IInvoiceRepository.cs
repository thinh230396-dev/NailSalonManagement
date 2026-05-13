using NailSalon.Domain.Entities;

namespace NailSalon.Domain.Interfaces.Repositories;

public interface IInvoiceRepository : IGenericRepository<Invoice>
{
    // Lấy chi tiết hóa đơn bao gồm cả các dịch vụ bên trong
    Task<Invoice?> GetInvoiceWithDetailsAsync(Guid invoiceId);

    // Lấy danh sách hóa đơn theo khoảng thời gian (Dùng cho module Thống kê doanh thu)
    Task<IEnumerable<Invoice>> GetInvoicesByDateRangeAsync(DateTime startDate, DateTime endDate);
}