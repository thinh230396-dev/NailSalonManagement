namespace NailSalon.Application.Features.Appointments.DTOs;

public class CreateAppointmentDto
{
    public DateTime AppointmentTime { get; set; }

    public string? Notes { get; set; }

    public Guid CustomerId { get; set; }

    public Guid EmployeeId { get; set; }
}