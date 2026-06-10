using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Features.Auth.DTOs;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Application.Interfaces.Services;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;

    public RefreshTokenHandler(
        IUnitOfWork unitOfWork,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
    }

    public async Task<AuthResponseDto> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        // lấy refresh token từ database để kiểm tra
        var sessions = await _unitOfWork.Repository<UserSession>().GetAllAsync();

        var oldSession = sessions.FirstOrDefault(x =>
            x.RefreshToken == request.RefreshToken);

        if (oldSession == null)
            throw new BadRequestException("Refresh token is invalid.");

        // kiểm tra refresh token còn hiệu lực hay không
        if (!oldSession.IsActive())
            throw new BadRequestException("Refresh token is expired or revoked.");

        var user = await _unitOfWork.Repository<User>().GetByIdAsync(oldSession.UserId);

        if (user == null)
            throw new NotFoundException(nameof(User), oldSession.UserId);

        if (!user.IsActive)
            throw new BadRequestException("Account is inactive.");

        var role = await _unitOfWork.Repository<Role>().GetByIdAsync(user.RoleId);

        oldSession.Revoke();

        var newAccessToken = _jwtTokenGenerator.GenerateToken(user);
        var newRefreshToken = _refreshTokenGenerator.GenerateRefreshToken();

        var accessTokenExpiredAt = DateTime.UtcNow.AddMinutes(3);
        var refreshTokenExpiredAt = DateTime.UtcNow.AddMinutes(10);

        var newSession = new UserSession(
            user.Id,
            newRefreshToken,
            refreshTokenExpiredAt,
            null,
            null);

        var auditLog = AuditLog.CreateAction(
            user.Id,
            "RefreshToken",
            "UserSession",
            oldSession.Id.ToString(),
            null,
            null);

        _unitOfWork.Repository<UserSession>().Update(oldSession);

        await _unitOfWork.Repository<UserSession>().AddAsync(newSession);
        await _unitOfWork.Repository<AuditLog>().AddAsync(auditLog);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
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