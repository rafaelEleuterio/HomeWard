using HomeWard.Application.Dtos;

namespace HomeWard.Application.Auth;

public interface ITokenService
{
    string GenerateToken(UserDto user);
}
