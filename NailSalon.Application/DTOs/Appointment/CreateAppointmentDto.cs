using NailSalon.Domain.Enums;

namespace NailSalon.Application.DTOs.Appointment;

public class CreateUpdateAppointmentDto
{
    public DateTime AppointmentTime { get; set; }
    public string? Notes { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

    public Guid CustomerId { get; set; }
    public Guid EmployeeId { get; set; }
}