using Microsoft.EntityFrameworkCore;
using NailSalon.Domain.Common;
using NailSalon.Domain.Interfaces.Repositories;
using NailSalon.Infrastructure.Data;

namespace NailSalon.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly NailSalonDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(NailSalonDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
        // Nhờ có AuditableEntityInterceptor ta đã viết, lệnh Remove này sẽ tự động bị chặn lại và biến thành Soft Delete (IsDeleted = true).
    }
}