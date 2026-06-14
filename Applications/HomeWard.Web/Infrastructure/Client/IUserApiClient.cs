using HomeWard.Application.Users;
using HomeWard.Domain.Dtos;

namespace HomeWard.Web.Infrastructure.Client;

public interface IUserApiClient
{
    Task<IReadOnlyList<UserDto>> GetUsersAsync(CancellationToken cancellationToken = default);
    Task<UserDto> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default);
    Task SetUserStatusAsync(Guid userId, bool isActive, CancellationToken cancellationToken = default);
}
