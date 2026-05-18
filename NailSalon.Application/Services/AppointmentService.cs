using AutoMapper;
using NailSalon.Application.DTOs.Appointment;
using NailSalon.Application.Interfaces.Services;
using NailSalon.Domain.Entities;
using NailSalon.Domain.Enums;
using NailSalon.Application.Interfaces.Repositories;

namespace NailSalon.Application.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync()
    {
        var appointments = await _unitOfWork.Appointments.GetAllAsync();
        return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
    }

    public async Task<AppointmentDto?> GetAppointmentByIdAsync(Guid id)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
        return _mapper.Map<AppointmentDto>(appointment);
    }

    public async Task<AppointmentDto> CreateAppointmentAsync(CreateUpdateAppointmentDto dto)
    {
        // 1. Kiểm tra thời gian không được ở quá khứ
        if (dto.AppointmentTime < DateTime.Now)
            throw new Exception("Không thể đặt lịch trong quá khứ.");

        // 2. Kiểm tra trùng lịch (Giả sử mỗi dịch vụ chiếm 45 phút)
        var isAvailable = await _unitOfWork.Appointments.IsTimeSlotAvailableAsync(dto.EmployeeId, dto.AppointmentTime, 45);
        if (!isAvailable)
            throw new Exception("Nhân viên này đã có lịch hẹn trong khung giờ bạn chọn.");

        var appointment = _mapper.Map<Appointment>(dto);

        await _unitOfWork.Appointments.AddAsync(appointment);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<AppointmentDto>(appointment);
    }

    public async Task UpdateStatusAsync(Guid id, int status)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
        if (appointment == null) throw new Exception("Không tìm thấy lịch hẹn.");

        // Ép kiểu từ số nguyên (int) sang Enum AppointmentStatus
        appointment.Status = (AppointmentStatus)status;

        _unitOfWork.Appointments.Update(appointment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CancelAppointmentAsync(Guid id)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
        if (appointment == null) throw new Exception("Không tìm thấy lịch hẹn.");

        // Thay vì xóa hẳn (Delete), đối với lịch hẹn ta thường chuyển trạng thái sang Cancelled (Hủy)
        // Giả sử trong Enum của bạn: 3 = Cancelled
        appointment.Status = (AppointmentStatus)3;

        _unitOfWork.Appointments.Update(appointment);
        await _unitOfWork.SaveChangesAsync();
    }
}