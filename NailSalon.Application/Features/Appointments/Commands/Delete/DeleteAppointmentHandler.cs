using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Appointments.Commands.Delete;

public class DeleteAppointmentHandler : IRequestHandler<DeleteAppointmentCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAppointmentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(request.Id);

        if (appointment == null)
            throw new NotFoundException(nameof(Appointment), request.Id);

        _unitOfWork.Repository<Appointment>().Delete(appointment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}