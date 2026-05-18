using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Customers.Commands.Update;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCustomerHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(request.Id);

        if (customer == null)
            throw new NotFoundException(nameof(Customer), request.Id);

        customer.FullName = request.FullName;
        customer.PhoneNumber = request.PhoneNumber;
        customer.Email = request.Email;
        customer.LoyaltyPoints = request.LoyaltyPoints;

        _unitOfWork.Repository<Customer>().Update(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}