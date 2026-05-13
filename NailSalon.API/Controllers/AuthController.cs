using Microsoft.AspNetCore.Mvc;
using NailSalon.Application.DTOs.Auth;
using NailSalon.Application.Interfaces;
using NailSalon.Application.Services;

namespace NailSalon.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        try
        {
            var result = await _authService.RegisterAsync(dto);
            return Ok(new { message = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            var response = await _authService.LoginAsync(dto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            // Trả về Unauthorized (401) nếu sai tài khoản/mật khẩu
            return Unauthorized(new { message = ex.Message });
        }
    }
}