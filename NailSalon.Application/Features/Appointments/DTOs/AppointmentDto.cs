using NailSalon.Domain.Enums;

namespace NailSalon.Application.Features.Appointments.DTOs;

public class AppointmentDto
{
    public Guid Id { get; set; }

    public DateTime AppointmentTime { get; set; }

    public AppointmentStatus Status { get; set; }

    public string? Notes { get; set; }

    public Guid CustomerId { get; set; }

    public Guid EmployeeId { get; set; }
}