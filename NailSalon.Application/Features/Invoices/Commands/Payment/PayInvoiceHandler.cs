using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Invoices.Commands.Payment;

public class PayInvoiceHandler : IRequestHandler<PayInvoiceCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public PayInvoiceHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(PayInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _unitOfWork.Repository<Invoice>().GetByIdAsync(request.InvoiceId);

        if (invoice == null)
            throw new NotFoundException(nameof(Invoice), request.InvoiceId);

        invoice.PaymentDate = DateTime.UtcNow;

        _unitOfWork.Repository<Invoice>().Update(invoice);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}