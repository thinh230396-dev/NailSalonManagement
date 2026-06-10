using MediatR;
using Microsoft.AspNetCore.Mvc;
using NailSalon.Application.Common.Models;
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
        var result = await _mediator.Send(new GetEmployeeListQuery());

        return Ok(ApiResponse<object>.Ok(
            result,
            "Lấy danh sách nhân viên thành công"));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeByIdQuery(id));

        return Ok(ApiResponse<object>.Ok(
            result,
            "Lấy thông tin nhân viên thành công"));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(ApiResponse<object>.Created(
            new { id },
            "Tạo nhân viên thành công"));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateEmployeeCommand command)
    {
        command.Id = id;

        await _mediator.Send(command);

        return Ok(ApiResponse<object>.Ok(
            new { id },
            "Cập nhật nhân viên thành công"));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteEmployeeCommand(id));

        return Ok(ApiResponse<object>.Ok(
            new { id },
            "Xóa nhân viên thành công"));
    }
}