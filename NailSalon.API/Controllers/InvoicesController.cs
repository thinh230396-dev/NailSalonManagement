using MediatR;
using Microsoft.AspNetCore.Mvc;
using NailSalon.Application.Features.Invoices.Commands.AddService;
using NailSalon.Application.Features.Invoices.Commands.Create;
using NailSalon.Application.Features.Invoices.Commands.Delete;
using NailSalon.Application.Features.Invoices.Commands.Payment;
using NailSalon.Application.Features.Invoices.Commands.Update;
using NailSalon.Application.Features.Invoices.Queries.GetById;
using NailSalon.Application.Features.Invoices.Queries.GetList;

namespace NailSalon.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvoicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        return Ok(await _mediator.Send(new GetInvoiceListQuery()));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _mediator.Send(new GetInvoiceByIdQuery(id)));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateInvoiceCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateInvoiceCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/services")]
    public async Task<IActionResult> AddService(Guid id, AddServiceToInvoiceCommand command)
    {
        command.InvoiceId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id:guid}/pay")]
    public async Task<IActionResult> Pay(Guid id)
    {
        await _mediator.Send(new PayInvoiceCommand { InvoiceId = id });
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteInvoiceCommand(id));
        return NoContent();
    }
}