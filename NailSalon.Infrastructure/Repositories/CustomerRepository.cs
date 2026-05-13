using Microsoft.EntityFrameworkCore;
using NailSalon.Domain.Entities;
using NailSalon.Domain.Interfaces.Repositories;
using NailSalon.Infrastructure.Data;

namespace NailSalon.Infrastructure.Repositories;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(NailSalonDbContext context) : base(context)
    {
    }

    public async Task<Customer?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
    }
}