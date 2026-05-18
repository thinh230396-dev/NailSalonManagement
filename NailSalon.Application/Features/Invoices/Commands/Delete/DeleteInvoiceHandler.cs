using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Invoices.Commands.Delete;

public class DeleteInvoiceHandler : IRequestHandler<DeleteInvoiceCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteInvoiceHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _unitOfWork.Repository<Invoice>().GetByIdAsync(request.Id);

        if (invoice == null)
            throw new NotFoundException(nameof(Invoice), request.Id);

        _unitOfWork.Repository<Invoice>().Delete(invoice);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}