using AutoMapper;
using NailSalon.Application.DTOs.Invoice;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Mappings;

public class InvoiceProfile : Profile
{
    public InvoiceProfile()
    {
        CreateMap<Invoice, InvoiceDto>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName));

        CreateMap<CreateInvoiceDto, Invoice>();
    }
}