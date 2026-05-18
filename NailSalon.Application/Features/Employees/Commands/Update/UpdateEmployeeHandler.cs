using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Employees.Commands.Update;

public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEmployeeHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.Id);

        if (employee == null)
            throw new NotFoundException(nameof(Employee), request.Id);

        employee.FullName = request.FullName;
        employee.PhoneNumber = request.PhoneNumber;
        employee.Position = request.Position;

        _unitOfWork.Repository<Employee>().Update(employee);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}