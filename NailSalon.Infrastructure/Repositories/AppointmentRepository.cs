using Microsoft.EntityFrameworkCore;
using NailSalon.Domain.Entities;
using NailSalon.Domain.Enums;
using NailSalon.Domain.Interfaces.Repositories;
using NailSalon.Infrastructure.Data;

namespace NailSalon.Infrastructure.Repositories;

public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(NailSalonDbContext context) : base(context)
    {
    }

    public async Task<bool> IsTimeSlotAvailableAsync(Guid employeeId, DateTime startTime, int durationMinutes)
    {
        var endTime = startTime.AddMinutes(durationMinutes);

        // Trả về true nếu KHÔNG CÓ bất kỳ lịch hẹn nào bị trùng lấn thời gian
        var hasConflict = await _dbSet
            .AnyAsync(a => a.EmployeeId == employeeId
                        && a.Status != AppointmentStatus.Cancelled
                        && a.AppointmentTime < endTime
                        && a.AppointmentTime.AddMinutes(30 /* Giả sử mặc định 30p nếu không join bảng NailService, thực tế bạn sẽ join để lấy Duration chính xác */) > startTime);

        return !hasConflict;
    }
}