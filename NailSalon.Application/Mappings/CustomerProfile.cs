using AutoMapper;
using NailSalon.Application.DTOs.Customer;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Mappings;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerDto>();

        // Map chung cho cả Create và Update
        CreateMap<CreateUpdateCustomerDto, Customer>();
    }
}