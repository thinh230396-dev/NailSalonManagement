using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Invoices.Commands.AddService;

public class AddServiceToInvoiceHandler : IRequestHandler<AddServiceToInvoiceCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddServiceToInvoiceHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(AddServiceToInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _unitOfWork.Repository<Invoice>().GetByIdAsync(request.InvoiceId);

        if (invoice == null)
            throw new NotFoundException(nameof(Invoice), request.InvoiceId);

        var nailService = await _unitOfWork.Repository<NailService>().GetByIdAsync(request.NailServiceId);

        if (nailService == null)
            throw new NotFoundException(nameof(NailService), request.NailServiceId);

        var invoiceDetail = new InvoiceDetail
        {
            Id = Guid.NewGuid(),
            InvoiceId = request.InvoiceId,
            NailServiceId = request.NailServiceId,
            Quantity = request.Quantity,
            UnitPrice = nailService.Price
        };

        await _unitOfWork.Repository<InvoiceDetail>().AddAsync(invoiceDetail);

        invoice.TotalAmount += invoiceDetail.Quantity * invoiceDetail.UnitPrice;

        _unitOfWork.Repository<Invoice>().Update(invoice);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}