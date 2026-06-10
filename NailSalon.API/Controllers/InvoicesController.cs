using MediatR;
using Microsoft.AspNetCore.Mvc;
using NailSalon.Application.Common.Models;
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
        var result = await _mediator.Send(new GetInvoiceListQuery());

        return Ok(ApiResponse<object>.Ok(
            result,
            "Lấy danh sách hóa đơn thành công"));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetInvoiceByIdQuery(id));

        return Ok(ApiResponse<object>.Ok(
            result,
            "Lấy thông tin hóa đơn thành công"));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInvoiceCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(ApiResponse<object>.Created(
            new { id },
            "Tạo hóa đơn thành công"));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateInvoiceCommand command)
    {
        command.Id = id;

        await _mediator.Send(command);

        return Ok(ApiResponse<object>.Ok(
            new { id },
            "Cập nhật hóa đơn thành công"));
    }

    [HttpPost("{id:guid}/services")]
    public async Task<IActionResult> AddService(
        Guid id,
        [FromBody] AddServiceToInvoiceCommand command)
    {
        command.InvoiceId = id;

        await _mediator.Send(command);

        return Ok(ApiResponse<object>.Ok(
            new
            {
                invoiceId = id,
                serviceId = command.NailServiceId,
                quantity = command.Quantity
            },
            "Thêm dịch vụ vào hóa đơn thành công"));
    }

    [HttpPut("{id:guid}/pay")]
    public async Task<IActionResult> Pay(Guid id)
    {
        await _mediator.Send(new PayInvoiceCommand
        {
            InvoiceId = id
        });

        return Ok(ApiResponse<object>.Ok(
            new { id },
            "Thanh toán hóa đơn thành công"));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteInvoiceCommand(id));

        return Ok(ApiResponse<object>.Ok(
            new { id },
            "Xóa hóa đơn thành công"));
    }
}