using AutoMapper;
using NailSalon.Application.DTOs.Invoice;
using NailSalon.Application.Interfaces;
using NailSalon.Domain.Entities;
using NailSalon.Domain.Interfaces.Repositories;

namespace NailSalon.Application.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public InvoiceService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync()
    {
        var invoices = await _unitOfWork.Repository<Invoice>().GetAllAsync();
        return _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
    }

    public async Task<InvoiceDto?> GetInvoiceByIdAsync(Guid id)
    {
        var invoice = await _unitOfWork.Repository<Invoice>().GetByIdAsync(id);
        return _mapper.Map<InvoiceDto>(invoice);
    }

    public async Task<InvoiceDto> CreateInvoiceAsync(CreateInvoiceDto dto)
    {
        // 1. Tạo hóa đơn
        var invoice = _mapper.Map<Invoice>(dto);

        // Tự động sinh mã hóa đơn (Ví dụ: HD-20260512-XXXX)
        invoice.InvoiceCode = $"HD-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";

        await _unitOfWork.Repository<Invoice>().AddAsync(invoice);

        // 2. Logic nghiệp vụ: Cộng điểm tích lũy cho Khách hàng (100k = 1 điểm)
        var customer = await _unitOfWork.Customers.GetByIdAsync(dto.CustomerId);
        if (customer != null)
        {
            int pointsEarned = (int)(dto.TotalAmount / 100000);
            customer.LoyaltyPoints += pointsEarned;
            _unitOfWork.Customers.Update(customer);
        }

        // 3. Lưu tất cả thay đổi (Lưu hóa đơn + Lưu điểm khách hàng) trong 1 Transaction
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<InvoiceDto>(invoice);
    }
}