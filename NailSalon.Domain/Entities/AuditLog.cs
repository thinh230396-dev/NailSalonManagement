using NailSalon.Domain.Common;

namespace NailSalon.Domain.Entities;

public class AuditLog : BaseEntity
{
    public Guid? UserId { get; private set; }
    public virtual User? User { get; private set; }

    public string Action { get; private set; } = string.Empty;

    public string EntityName { get; private set; } = string.Empty;

    public string? EntityId { get; private set; }

    public string? IpAddress { get; private set; }

    public string? DeviceInfo { get; private set; }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private AuditLog()
    {
    }

    private AuditLog(
        Guid? userId,
        string action,
        string entityName,
        string? entityId,
        string? ipAddress,
        string? deviceInfo)
    {
        if (string.IsNullOrWhiteSpace(action))
            throw new ArgumentException("Action không được để trống.");

        if (string.IsNullOrWhiteSpace(entityName))
            throw new ArgumentException("EntityName không được để trống.");

        UserId = userId;
        Action = action;
        EntityName = entityName;
        EntityId = entityId;
        IpAddress = ipAddress;
        DeviceInfo = deviceInfo;
        CreatedAt = DateTime.UtcNow;
    }

    public static AuditLog CreateLoginSuccess(
        Guid userId,
        string? ipAddress,
        string? deviceInfo)
    {
        return new AuditLog(
            userId,
            "LoginSuccess",
            "User",
            userId.ToString(),
            ipAddress,
            deviceInfo);
    }

    public static AuditLog CreateLoginFailed(
        string username,
        string? ipAddress,
        string? deviceInfo)
    {
        return new AuditLog(
            null,
            "LoginFailed",
            "User",
            username,
            ipAddress,
            deviceInfo);
    }

    public static AuditLog CreateLogout(
        Guid userId,
        string? ipAddress,
        string? deviceInfo)
    {
        return new AuditLog(
            userId,
            "Logout",
            "UserSession",
            userId.ToString(),
            ipAddress,
            deviceInfo);
    }

    public static AuditLog CreateSessionRevoked(
        Guid userId,
        Guid sessionId,
        string? ipAddress,
        string? deviceInfo)
    {
        return new AuditLog(
            userId,
            "SessionRevoked",
            "UserSession",
            sessionId.ToString(),
            ipAddress,
            deviceInfo);
    }

    public static AuditLog CreateAction(
        Guid? userId,
        string action,
        string entityName,
        string? entityId,
        string? ipAddress,
        string? deviceInfo)
    {
        return new AuditLog(
            userId,
            action,
            entityName,
            entityId,
            ipAddress,
            deviceInfo);
    }
}