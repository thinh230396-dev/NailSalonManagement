using AutoMapper;
using NailSalon.Application.DTOs.Customer;
using NailSalon.Application.Interfaces;
using NailSalon.Domain.Entities;
using NailSalon.Domain.Interfaces.Repositories;

namespace NailSalon.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
    {
        var customers = await _unitOfWork.Customers.GetAllAsync();
        return _mapper.Map<IEnumerable<CustomerDto>>(customers);
    }

    public async Task<CustomerDto?> GetCustomerByIdAsync(Guid id)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(id);
        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<CustomerDto> CreateCustomerAsync(CreateUpdateCustomerDto dto)
    {
        var existingCustomer = await _unitOfWork.Customers.GetByPhoneNumberAsync(dto.PhoneNumber);
        if (existingCustomer != null)
        {
            throw new Exception("Số điện thoại này đã được đăng ký.");
        }

        var customer = _mapper.Map<Customer>(dto);

        await _unitOfWork.Customers.AddAsync(customer);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task UpdateCustomerAsync(Guid id, CreateUpdateCustomerDto dto)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(id);
        if (customer == null) throw new Exception("Không tìm thấy khách hàng.");

        // Map đè dữ liệu từ DTO sang Entity hiện tại
        _mapper.Map(dto, customer);

        _unitOfWork.Customers.Update(customer);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteCustomerAsync(Guid id)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(id);
        if (customer == null) throw new Exception("Không tìm thấy khách hàng.");

        _unitOfWork.Customers.Delete(customer);
        await _unitOfWork.SaveChangesAsync();
    }
}