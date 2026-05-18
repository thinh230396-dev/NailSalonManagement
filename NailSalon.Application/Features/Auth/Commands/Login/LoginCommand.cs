using MediatR;
using NailSalon.Application.Features.Auth.DTOs;

namespace NailSalon.Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<AuthResponseDto>
{
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}