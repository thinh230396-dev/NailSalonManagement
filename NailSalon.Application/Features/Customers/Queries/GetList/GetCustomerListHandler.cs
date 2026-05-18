using MediatR;
using NailSalon.Application.Features.Customers.DTOs;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Customers.Queries.GetList;

public class GetCustomerListHandler : IRequestHandler<GetCustomerListQuery, List<CustomerDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCustomerListHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CustomerDto>> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
    {
        var customers = await _unitOfWork.Repository<Customer>().GetAllAsync();

        return customers.Select(x => new CustomerDto
        {
            Id = x.Id,
            FullName = x.FullName,
            PhoneNumber = x.PhoneNumber,
            Email = x.Email,
            LoyaltyPoints = x.LoyaltyPoints
        }).ToList();
    }
}