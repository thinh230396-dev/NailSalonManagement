using MediatR;
using NailSalon.Application.Features.Invoices.DTOs;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Invoices.Queries.GetList;

public class GetInvoiceListHandler : IRequestHandler<GetInvoiceListQuery, List<InvoiceDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetInvoiceListHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<InvoiceDto>> Handle(GetInvoiceListQuery request, CancellationToken cancellationToken)
    {
        var invoices = await _unitOfWork.Repository<Invoice>().GetAllAsync();

        return invoices.Select(x => new InvoiceDto
        {
            Id = x.Id,
            InvoiceCode = x.InvoiceCode,
            PaymentDate = x.PaymentDate,
            TotalAmount = x.TotalAmount,
            CustomerId = x.CustomerId,
            EmployeeId = x.EmployeeId
        }).ToList();
    }
}