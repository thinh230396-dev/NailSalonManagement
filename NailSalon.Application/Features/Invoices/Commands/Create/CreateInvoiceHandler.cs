using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Invoices.Commands.Create;

public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateInvoiceHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(request.CustomerId);

        if (customer == null)
            throw new NotFoundException(nameof(Customer), request.CustomerId);

        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.EmployeeId);

        if (employee == null)
            throw new NotFoundException(nameof(Employee), request.EmployeeId);

        var invoice = new Invoice
        {
            Id = Guid.NewGuid(),
            InvoiceCode = request.InvoiceCode,
            CustomerId = request.CustomerId,
            EmployeeId = request.EmployeeId,
            PaymentDate = DateTime.UtcNow,
            TotalAmount = 0
        };

        await _unitOfWork.Repository<Invoice>().AddAsync(invoice);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return invoice.Id;
    }
}