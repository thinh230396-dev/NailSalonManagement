using NailSalon.Application.DTOs.Appointment;

namespace NailSalon.Application.Interfaces;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();
    Task<AppointmentDto?> GetAppointmentByIdAsync(Guid id);
    Task<AppointmentDto> CreateAppointmentAsync(CreateUpdateAppointmentDto dto);
    Task UpdateStatusAsync(Guid id, int status);
    Task CancelAppointmentAsync(Guid id);
}