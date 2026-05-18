using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Appointments.Commands.ChangeStatus;

public class ChangeAppointmentStatusHandler : IRequestHandler<ChangeAppointmentStatusCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public ChangeAppointmentStatusHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(ChangeAppointmentStatusCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(request.Id);

        if (appointment == null)
            throw new NotFoundException(nameof(Appointment), request.Id);

        appointment.Status = request.Status;

        _unitOfWork.Repository<Appointment>().Update(appointment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}