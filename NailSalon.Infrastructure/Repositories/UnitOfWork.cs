using System.Collections;
using NailSalon.Domain.Common;
using NailSalon.Domain.Interfaces.Repositories;
using NailSalon.Infrastructure.Data;

namespace NailSalon.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly NailSalonDbContext _context;
    private Hashtable _repositories;
    private IAppointmentRepository _appointmentRepository;

    public UnitOfWork(NailSalonDbContext context)
    {
        _context = context;
        _repositories = new Hashtable();
    }

    // Lazy initialization cho các repo đặc thù
    public IAppointmentRepository Appointments =>
        _appointmentRepository ??= new AppointmentRepository(_context);

    // Cấp phát Generic Repository linh hoạt
    public IGenericRepository<T> Repository<T>() where T : BaseEntity
    {
        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(
                repositoryType.MakeGenericType(typeof(T)), _context);

            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<T>)_repositories[type]!;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Chỗ này sẽ kích hoạt cái AuditableEntityInterceptor để tự điền CreatedAt, UpdatedAt
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    private ICustomerRepository _customerRepository;
    private IInvoiceRepository _invoiceRepository;

    public ICustomerRepository Customers =>
        _customerRepository ??= new CustomerRepository(_context);

    public IInvoiceRepository Invoices =>
        _invoiceRepository ??= new InvoiceRepository(_context);
}