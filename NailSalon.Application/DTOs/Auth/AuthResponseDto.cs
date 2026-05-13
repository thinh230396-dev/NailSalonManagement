namespace NailSalon.Application.DTOs.Auth;

// BẮT BUỘC phải có chữ "public" ở đây
public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}