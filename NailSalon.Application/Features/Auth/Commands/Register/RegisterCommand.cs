using MediatR;
using NailSalon.Application.Features.Auth.DTOs;

namespace NailSalon.Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<AuthResponseDto>
{
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public Guid RoleId { get; set; }

    public Guid? EmployeeId { get; set; }
}