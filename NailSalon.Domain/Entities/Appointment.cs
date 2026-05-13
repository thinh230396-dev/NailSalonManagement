using NailSalon.Domain.Common;
using NailSalon.Domain.Enums;

namespace NailSalon.Domain.Entities;

public class Appointment : BaseEntity
{
    public DateTime AppointmentTime { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    public string? Notes { get; set; }

    // Foreign Keys
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = null!;

    public Guid EmployeeId { get; set; }
    public virtual Employee Employee { get; set; } = null!;
}