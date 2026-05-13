using NailSalon.Application.DTOs.Invoice;

namespace NailSalon.Application.Interfaces;

public interface IInvoiceService
{
    Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync();
    Task<InvoiceDto?> GetInvoiceByIdAsync(Guid id);
    Task<InvoiceDto> CreateInvoiceAsync(CreateInvoiceDto dto);
}