using MediatR;
using Microsoft.AspNetCore.Mvc;
using NailSalon.Application.Features.Employees.Commands.Create;
using NailSalon.Application.Features.Employees.Commands.Delete;
using NailSalon.Application.Features.Employees.Commands.Update;
using NailSalon.Application.Features.Employees.Queries.GetById;
using NailSalon.Application.Features.Employees.Queries.GetList;

namespace NailSalon.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        return Ok(await _mediator.Send(new GetEmployeeListQuery()));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _mediator.Send(new GetEmployeeByIdQuery(id)));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateEmployeeCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteEmployeeCommand(id));
        return NoContent();
    }
}