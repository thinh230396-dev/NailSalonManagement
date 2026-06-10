using MediatR;
using Microsoft.AspNetCore.Mvc;
using NailSalon.Application.Common.Models;
using NailSalon.Application.Features.Appointments.Commands.ChangeStatus;
using NailSalon.Application.Features.Appointments.Commands.Create;
using NailSalon.Application.Features.Appointments.Commands.Delete;
using NailSalon.Application.Features.Appointments.Commands.Update;
using NailSalon.Application.Features.Appointments.Queries.GetByDate;
using NailSalon.Application.Features.Appointments.Queries.GetById;
using NailSalon.Application.Features.Appointments.Queries.GetList;

namespace NailSalon.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppointmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var result = await _mediator.Send(new GetAppointmentListQuery());

        return Ok(ApiResponse<object>.Ok(
            result,
            "Lấy danh sách lịch hẹn thành công"));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetAppointmentByIdQuery(id));

        return Ok(ApiResponse<object>.Ok(
            result,
            "Lấy thông tin lịch hẹn thành công"));
    }

    [HttpGet("by-date")]
    public async Task<IActionResult> GetByDate([FromQuery] DateTime date)
    {
        var result = await _mediator.Send(new GetAppointmentsByDateQuery(date));

        return Ok(ApiResponse<object>.Ok(
            result,
            "Lấy lịch hẹn theo ngày thành công"));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(ApiResponse<object>.Created(
            new { id },
            "Tạo lịch hẹn thành công"));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateAppointmentCommand command)
    {
        command.Id = id;

        await _mediator.Send(command);

        return Ok(ApiResponse<object>.Ok(
            new { id },
            "Cập nhật lịch hẹn thành công"));
    }

    [HttpPut("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(
        Guid id,
        [FromBody] ChangeAppointmentStatusCommand command)
    {
        command.Id = id;

        await _mediator.Send(command);

        return Ok(ApiResponse<object>.Ok(
            new { id, command.Status },
            "Cập nhật trạng thái lịch hẹn thành công"));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteAppointmentCommand(id));

        return Ok(ApiResponse<object>.Ok(
            new { id },
            "Xóa lịch hẹn thành công"));
    }
}