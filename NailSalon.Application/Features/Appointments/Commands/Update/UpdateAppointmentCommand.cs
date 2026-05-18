using MediatR;

namespace NailSalon.Application.Features.Appointments.Commands.Update;

public class UpdateAppointmentCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public DateTime AppointmentTime { get; set; }

    public string? Notes { get; set; }

    public Guid CustomerId { get; set; }

    public Guid EmployeeId { get; set; }
}