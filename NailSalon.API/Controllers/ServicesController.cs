using MediatR;
using Microsoft.AspNetCore.Mvc;
using NailSalon.Application.Features.NailServices.Commands.Create;
using NailSalon.Application.Features.NailServices.Commands.Delete;
using NailSalon.Application.Features.NailServices.Commands.Update;
using NailSalon.Application.Features.NailServices.Queries.GetById;
using NailSalon.Application.Features.NailServices.Queries.GetList;

namespace NailSalon.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var result = await _mediator.Send(new GetNailServiceListQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetNailServiceByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateNailServiceCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateNailServiceCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteNailServiceCommand(id));
        return NoContent();
    }
}