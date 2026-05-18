using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Appointments.Commands.Update;

public class UpdateAppointmentHandler : IRequestHandler<UpdateAppointmentCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAppointmentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(request.Id);

        if (appointment == null)
            throw new NotFoundException(nameof(Appointment), request.Id);

        var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(request.CustomerId);
        if (customer == null)
            throw new NotFoundException(nameof(Customer), request.CustomerId);

        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.EmployeeId);
        if (employee == null)
            throw new NotFoundException(nameof(Employee), request.EmployeeId);

        appointment.AppointmentTime = request.AppointmentTime;
        appointment.Notes = request.Notes;
        appointment.CustomerId = request.CustomerId;
        appointment.EmployeeId = request.EmployeeId;

        _unitOfWork.Repository<Appointment>().Update(appointment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}