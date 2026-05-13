using NailSalon.Domain.Enums;

namespace NailSalon.Application.DTOs.Appointment;

public class AppointmentDto
{
    public Guid Id { get; set; }
    public DateTime AppointmentTime { get; set; }
    public AppointmentStatus Status { get; set; }
    public string? Notes { get; set; }

    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;

    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
}