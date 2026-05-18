using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;
using NailSalon.Domain.Enums;

namespace NailSalon.Application.Features.Appointments.Commands.Create;

public class CreateAppointmentHandler : IRequestHandler<CreateAppointmentCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAppointmentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(request.CustomerId);
        if (customer == null)
            throw new NotFoundException(nameof(Customer), request.CustomerId);

        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.EmployeeId);
        if (employee == null)
            throw new NotFoundException(nameof(Employee), request.EmployeeId);

        var appointment = new Appointment
        {
            Id = Guid.NewGuid(),
            AppointmentTime = request.AppointmentTime,
            Notes = request.Notes,
            CustomerId = request.CustomerId,
            EmployeeId = request.EmployeeId,
            Status = AppointmentStatus.Pending
        };

        await _unitOfWork.Repository<Appointment>().AddAsync(appointment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return appointment.Id;
    }
}