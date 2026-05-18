using Microsoft.AspNetCore.Mvc;
using NailSalon.Application.DTOs.Appointment;
using NailSalon.Application.Interfaces.Services;
using NailSalon.Domain.Enums;

namespace NailSalon.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    // 1. Lấy danh sách tất cả lịch hẹn
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var appointments = await _appointmentService.GetAllAppointmentsAsync();
        return Ok(appointments);
    }

    // 2. Lấy chi tiết lịch hẹn theo ID
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
        if (appointment == null) return NotFound("Không tìm thấy lịch hẹn.");

        return Ok(appointment);
    }

    // 3. Đặt lịch hẹn mới (POST) - Nơi xử lý logic trùng lịch
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUpdateAppointmentDto dto)
    {
        try
        {
            var newAppointment = await _appointmentService.CreateAppointmentAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newAppointment.Id }, newAppointment);
        }
        catch (Exception ex)
        {
            // Trả về lỗi nếu trùng lịch hoặc thời gian không hợp lệ
            return BadRequest(new { message = ex.Message });
        }
    }

    // 4. Cập nhật trạng thái lịch hẹn (Ví dụ: Chuyển từ Pending sang Confirmed hoặc Completed)
    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromQuery] int status)
    {
        try
        {
            await _appointmentService.UpdateStatusAsync(id, status);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // 5. Hủy lịch hẹn
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        try
        {
            await _appointmentService.CancelAppointmentAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}