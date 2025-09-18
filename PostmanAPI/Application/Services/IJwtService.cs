using PostmanAPI.Domain.Entities;

namespace PostmanAPI.Application.Services;

public interface IJwtService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    int? ValidateToken(string token);
}
