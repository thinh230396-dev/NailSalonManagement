using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NailSalon.Application.Common.Models;
using NailSalon.Application.Features.Auth.Commands.Login;
using NailSalon.Application.Features.Auth.Commands.Logout;
using NailSalon.Application.Features.Auth.Commands.RefreshToken;
using NailSalon.Application.Features.Auth.Commands.Register;

namespace NailSalon.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(ApiResponse<object>.Ok(
            result,
            "Đăng ký tài khoản thành công"));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(ApiResponse<object>.Ok(
            result,
            "Đăng nhập thành công"));
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(ApiResponse<object>.Ok(
            result,
            "Cấp lại access token thành công"));
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutCommand command)
    {
        await _mediator.Send(command);

        return Ok(ApiResponse<object>.Ok(
            null,
            "Đăng xuất thành công"));
    }
}