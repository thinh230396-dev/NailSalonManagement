using NailSalon.Domain.Common;

namespace NailSalon.Application.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    // Lấy Generic Repository cho bất kỳ Entity nào
    IGenericRepository<T> Repository<T>() where T : BaseEntity;

    // Các Repository đặc thù
    IAppointmentRepository Appointments { get; }

    ICustomerRepository Customers { get; }
    IInvoiceRepository Invoices { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}