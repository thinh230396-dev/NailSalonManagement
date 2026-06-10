namespace NailSalon.Application.Features.Auth.DTOs;

public class AuthResponseDto
{
    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;

    public DateTime AccessTokenExpiredAt { get; set; }

    public DateTime RefreshTokenExpiredAt { get; set; }

    public UserInfoDto User { get; set; } = null!;
}