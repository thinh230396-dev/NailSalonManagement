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
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;

    public LoginHandler(
        IUnitOfWork unitOfWork,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
    }

    public async Task<AuthResponseDto> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.Repository<User>().GetAllAsync();

        var user = users.FirstOrDefault(x =>
            x.Username == request.Username &&
            x.PasswordHash == request.Password);

        if (user == null)
        {
            var failedLog = AuditLog.CreateLoginFailed(
                request.Username,
                null,
                null);

            await _unitOfWork.Repository<AuditLog>().AddAsync(failedLog);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            throw new BadRequestException("Username or password is incorrect.");
        }

        // Kiểm tra nếu tài khoản bị khóa hoặc không hoạt động
        if (!user.IsActive)
        {
            var inactiveLog = AuditLog.CreateAction(
                user.Id,
                "LoginFailedInactiveAccount",
                "User",
                user.Id.ToString(),
                null,
                null);

            await _unitOfWork.Repository<AuditLog>().AddAsync(inactiveLog);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            throw new BadRequestException("Account is inactive.");
        }

        var role = await _unitOfWork.Repository<Role>().GetByIdAsync(user.RoleId);

        var accessToken = _jwtTokenGenerator.GenerateToken(user);
        var refreshToken = _refreshTokenGenerator.GenerateRefreshToken();

        var accessTokenExpiredAt = DateTime.UtcNow.AddMinutes(3);
        var refreshTokenExpiredAt = DateTime.UtcNow.AddMinutes(10);

        var session = new UserSession(
            user.Id,
            refreshToken,
            refreshTokenExpiredAt,
            null,
            null);

        user.MarkLoginSuccess();

        var successLog = AuditLog.CreateLoginSuccess(
            user.Id,
            null,
            null);

        await _unitOfWork.Repository<UserSession>().AddAsync(session);
        await _unitOfWork.Repository<AuditLog>().AddAsync(successLog);

        _unitOfWork.Repository<User>().Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiredAt = accessTokenExpiredAt,
            RefreshTokenExpiredAt = refreshTokenExpiredAt,

            User = new UserInfoDto
            {
                Id = user.Id,
                Username = user.Username,
                RoleName = role?.RoleName ?? string.Empty,
                EmployeeId = user.EmployeeId,
                IsActive = user.IsActive,
                LastLoginAt = user.LastLoginAt
            }
        };
    }
}