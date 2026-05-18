using MediatR;
using NailSalon.Domain.Enums;

namespace NailSalon.Application.Features.Appointments.Commands.ChangeStatus;

public class ChangeAppointmentStatusCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public AppointmentStatus Status { get; set; }
}