using MediatR;

namespace NailSalon.Application.Features.Appointments.Commands.Delete;

public class DeleteAppointmentCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public DeleteAppointmentCommand(Guid id)
    {
        Id = id;
    }
}