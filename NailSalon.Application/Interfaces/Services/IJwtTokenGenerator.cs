using NailSalon.Domain.Entities;

namespace NailSalon.Application.Interfaces.Services;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}