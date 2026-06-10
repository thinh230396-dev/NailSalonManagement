namespace NailSalon.Application.Interfaces.Services;

public interface IRefreshTokenGenerator
{
    string GenerateRefreshToken();
}