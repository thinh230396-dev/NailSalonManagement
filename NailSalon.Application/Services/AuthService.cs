using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using NailSalon.Application.DTOs.Auth;
using NailSalon.Application.Interfaces;
using NailSalon.Domain.Entities;
using NailSalon.Domain.Interfaces.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NailSalon.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        // Kiểm tra xem Username đã tồn tại chưa (Giả sử bạn dùng Generic Repository cho User)
        var users = await _unitOfWork.Repository<User>().GetAllAsync();
        if (users.Any(u => u.Username == dto.Username))
        {
            throw new Exception("Tên đăng nhập đã tồn tại.");
        }

        // Mã hóa mật khẩu bằng BCrypt
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var newUser = new User
        {
            Username = dto.Username,
            PasswordHash = passwordHash,
            EmployeeId = dto.EmployeeId,
            RoleId = dto.RoleId
        };

        await _unitOfWork.Repository<User>().AddAsync(newUser);
        await _unitOfWork.SaveChangesAsync();

        return "Đăng ký tài khoản thành công!";
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        // 1. Tìm user theo Username
        var users = await _unitOfWork.Repository<User>().GetAllAsync();
        var user = users.FirstOrDefault(u => u.Username == dto.Username);

        if (user == null)
            throw new Exception("Tài khoản không tồn tại.");

        // 2. Kiểm tra mật khẩu
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
        if (!isPasswordValid)
            throw new Exception("Mật khẩu không chính xác.");

        // 3. Tạo JWT Token
        var token = GenerateJwtToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Username = user.Username,
            Message = "Đăng nhập thành công"
        };
    }

    private string GenerateJwtToken(User user)
    {
        // Lấy secret key từ appsettings.json
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["Secret"] ?? throw new Exception("Thiếu cấu hình JWT Secret.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Chứa thông tin người dùng vào trong Token (Claims)
        var claims = new[]
        {
            // Thay vì dùng JwtRegisteredClaimNames.Sub dễ gây lỗi, ta dùng luôn chuỗi "sub"
            new Claim("sub", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("EmployeeId", user.EmployeeId.ToString() ?? "")
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2), // Token có hạn trong 2 giờ
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}