using Microsoft.EntityFrameworkCore;
using NailSalon.Domain.Entities;
using NailSalon.Domain.Interfaces.Repositories;
using NailSalon.Infrastructure.Data;

namespace NailSalon.Infrastructure.Repositories;

public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
{
    public InvoiceRepository(NailSalonDbContext context) : base(context)
    {
    }

    public async Task<Invoice?> GetInvoiceWithDetailsAsync(Guid invoiceId)
    {
        // Dùng Include để lấy luôn thông tin bảng con (Eager Loading)
        return await _dbSet
            .Include(x => x.InvoiceDetails)
                .ThenInclude(d => d.NailService)
            .Include(x => x.Customer)
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(x => x.Id == invoiceId);
    }

    public async Task<IEnumerable<Invoice>> GetInvoicesByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Where(x => x.PaymentDate >= startDate && x.PaymentDate <= endDate)
            .Include(x => x.InvoiceDetails) // Lấy kèm chi tiết để tính tổng tiền chính xác
            .ToListAsync();
    }
}