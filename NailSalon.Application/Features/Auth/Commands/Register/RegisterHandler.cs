using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Features.Auth.DTOs;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Application.Interfaces.Services;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Auth.Commands.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterHandler(
        IUnitOfWork unitOfWork,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.Repository<User>().GetAllAsync();

        var usernameExists = users.Any(x => x.Username == request.Username);

        if (usernameExists)
            throw new BadRequestException("Username already exists.");

        var role = await _unitOfWork.Repository<Role>().GetByIdAsync(request.RoleId);

        if (role == null)
            throw new NotFoundException(nameof(Role), request.RoleId);

        if (request.EmployeeId.HasValue)
        {
            var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.EmployeeId.Value);

            if (employee == null)
                throw new NotFoundException(nameof(Employee), request.EmployeeId.Value);
        }

        var user = new User(
            request.Username,
            request.Password,
            request.RoleId,
            request.EmployeeId);

        await _unitOfWork.Repository<User>().AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthResponseDto
        {
            AccessToken = token,
            RefreshToken = string.Empty,
            AccessTokenExpiredAt = DateTime.UtcNow.AddHours(2),
            RefreshTokenExpiredAt = DateTime.UtcNow,
            User = new UserInfoDto
            {
                Id = user.Id,
                Username = user.Username,
                RoleName = role.RoleName,
                EmployeeId = user.EmployeeId,
                IsActive = user.IsActive,
                LastLoginAt = user.LastLoginAt
            }
        };
    }
}