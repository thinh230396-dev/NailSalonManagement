using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Features.Auth.DTOs;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Application.Interfaces.Services;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Auth.Commands.Login;

public class LoginHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginHandler(
        IUnitOfWork unitOfWork,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.Repository<User>().GetAllAsync();

        var user = users.FirstOrDefault(x =>
            x.Username == request.Username &&
            x.PasswordHash == request.Password);

        if (user == null)
            throw new BadRequestException("Username or password is incorrect.");

        var roles = await _unitOfWork.Repository<Role>().GetAllAsync();

        var role = roles.FirstOrDefault(x => x.Id == user.RoleId);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthResponseDto
        {
            UserId = user.Id,
            Username = user.Username,
            RoleName = role?.RoleName ?? string.Empty,
            Token = token
        };
    }
}