using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Invoices.Commands.Update;

public class UpdateInvoiceHandler : IRequestHandler<UpdateInvoiceCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateInvoiceHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _unitOfWork.Repository<Invoice>().GetByIdAsync(request.Id);

        if (invoice == null)
            throw new NotFoundException(nameof(Invoice), request.Id);

        invoice.InvoiceCode = request.InvoiceCode;
        invoice.CustomerId = request.CustomerId;
        invoice.EmployeeId = request.EmployeeId;

        _unitOfWork.Repository<Invoice>().Update(invoice);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}