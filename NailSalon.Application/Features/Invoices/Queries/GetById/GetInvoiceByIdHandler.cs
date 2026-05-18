using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Features.Invoices.DTOs;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Invoices.Queries.GetById;

public class GetInvoiceByIdHandler : IRequestHandler<GetInvoiceByIdQuery, InvoiceDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetInvoiceByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<InvoiceDto> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        var invoice = await _unitOfWork.Repository<Invoice>().GetByIdAsync(request.Id);

        if (invoice == null)
            throw new NotFoundException(nameof(Invoice), request.Id);

        return new InvoiceDto
        {
            Id = invoice.Id,
            InvoiceCode = invoice.InvoiceCode,
            PaymentDate = invoice.PaymentDate,
            TotalAmount = invoice.TotalAmount,
            CustomerId = invoice.CustomerId,
            EmployeeId = invoice.EmployeeId
        };
    }
}