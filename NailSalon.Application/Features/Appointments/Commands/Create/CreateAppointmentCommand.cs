using MediatR;

namespace NailSalon.Application.Features.Appointments.Commands.Create;

public class CreateAppointmentCommand : IRequest<Guid>
{
    public DateTime AppointmentTime { get; set; }

    public string? Notes { get; set; }

    public Guid CustomerId { get; set; }

    public Guid EmployeeId { get; set; }
}