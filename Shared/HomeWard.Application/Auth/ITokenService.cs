using HomeWard.Domain.Dtos;

namespace HomeWard.Application.Auth;

public interface ITokenService
{
    string GenerateToken(UserDto user);
}
