using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        return Ok(await _mediator.Send(new GetAppointmentListQuery()));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _mediator.Send(new GetAppointmentByIdQuery(id)));
    }

    [HttpGet("by-date")]
    public async Task<IActionResult> GetByDate([FromQuery] DateTime date)
    {
        return Ok(await _mediator.Send(new GetAppointmentsByDateQuery(date)));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAppointmentCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateAppointmentCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, ChangeAppointmentStatusCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteAppointmentCommand(id));
        return NoContent();
    }
}