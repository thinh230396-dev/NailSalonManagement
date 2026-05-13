using NailSalon.Domain.Entities;

namespace NailSalon.Domain.Interfaces.Repositories;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    // Tìm khách hàng qua số điện thoại (Dùng cho module Tìm kiếm & Tích điểm)
    Task<Customer?> GetByPhoneNumberAsync(string phoneNumber);
}