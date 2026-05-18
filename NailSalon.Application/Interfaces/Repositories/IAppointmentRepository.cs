using NailSalon.Domain.Entities;

namespace NailSalon.Application.Interfaces.Repositories;

public interface IAppointmentRepository : IGenericRepository<Appointment>
{
    // Hàm đặc thù: Kiểm tra xem nhân viên có đang rảnh vào khung giờ đó không
    Task<bool> IsTimeSlotAvailableAsync(Guid employeeId, DateTime startTime, int durationMinutes);
}