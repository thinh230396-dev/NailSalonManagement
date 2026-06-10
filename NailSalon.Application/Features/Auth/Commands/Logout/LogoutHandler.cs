using MediatR;
using NailSalon.Application.Common.Exceptions;
using NailSalon.Application.Interfaces.Repositories;
using NailSalon.Domain.Entities;

namespace NailSalon.Application.Features.Auth.Commands.Logout;

public class LogoutHandler : IRequestHandler<LogoutCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public LogoutHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        LogoutCommand request,
        CancellationToken cancellationToken)
    {
        var sessions = await _unitOfWork.Repository<UserSession>().GetAllAsync();

        var session = sessions.FirstOrDefault(x =>
            x.RefreshToken == request.RefreshToken);

        if (session == null)
            throw new BadRequestException("Session not found.");

        session.Revoke();

        var auditLog = AuditLog.CreateLogout(
            session.UserId,
            null,
            null);

        _unitOfWork.Repository<UserSession>().Update(session);

        await _unitOfWork.Repository<AuditLog>().AddAsync(auditLog);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}