using MediatR;
using Microsoft.AspNetCore.Mvc;
using NailSalon.Application.Common.Models;
using NailSalon.Application.Features.Customers.Commands.Create;
using NailSalon.Application.Features.Customers.Commands.Delete;
using NailSalon.Application.Features.Customers.Commands.Update;
using NailSalon.Application.Features.Customers.Queries.GetById;
using NailSalon.Application.Features.Customers.Queries.GetList;

namespace NailSalon.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var result = await _mediator.Send(new GetCustomerListQuery());

        return Ok(ApiResponse<object>.Ok(
            result,
            "Lấy danh sách khách hàng thành công"));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetCustomerByIdQuery(id));

        return Ok(ApiResponse<object>.Ok(
            result,
            "Lấy thông tin khách hàng thành công"));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(ApiResponse<object>.Created(
            new { id },
            "Tạo khách hàng thành công"));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateCustomerCommand command)
    {
        command.Id = id;

        await _mediator.Send(command);

        return Ok(ApiResponse<object>.Ok(
            new { id },
            "Cập nhật khách hàng thành công"));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteCustomerCommand(id));

        return Ok(ApiResponse<object>.Ok(
            new { id },
            "Xóa khách hàng thành công"));
    }
}