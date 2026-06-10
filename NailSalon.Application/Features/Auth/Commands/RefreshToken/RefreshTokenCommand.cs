using MediatR;
using NailSalon.Application.Features.Auth.DTOs;

namespace NailSalon.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<AuthResponseDto>
{
    public string RefreshToken { get; set; } = string.Empty;
}