using NailSalon.Domain.Common;

namespace NailSalon.Domain.Entities;

public class UserSession : BaseEntity
{
    public Guid UserId { get; private set; }
    public virtual User User { get; private set; } = null!;

    public string RefreshToken { get; private set; } = string.Empty;

    public DateTime ExpiredAt { get; private set; }

    public bool IsRevoked { get; private set; }

    public string? IpAddress { get; private set; }

    public string? DeviceInfo { get; private set; }

    private UserSession()
    {
    }

    public UserSession(
        Guid userId,
        string refreshToken,
        DateTime expiredAt,
        string? ipAddress,
        string? deviceInfo)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId không hợp lệ.");

        if (string.IsNullOrWhiteSpace(refreshToken))
            throw new ArgumentException("Refresh token không được để trống.");

        if (expiredAt <= DateTime.UtcNow)
            throw new ArgumentException("Thời gian hết hạn phải lớn hơn hiện tại.");

        UserId = userId;
        RefreshToken = refreshToken;
        ExpiredAt = expiredAt;
        IpAddress = ipAddress;
        DeviceInfo = deviceInfo;
        IsRevoked = false;
    }

    public bool IsExpired()
    {
        return DateTime.UtcNow >= ExpiredAt;
    }

    public bool IsActive()
    {
        return !IsRevoked && !IsExpired();
    }

    public void Revoke()
    {
        if (IsRevoked)
            return;

        IsRevoked = true;
    }
}