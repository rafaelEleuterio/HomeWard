using HomeWard.Domain.Dtos;

namespace HomeWard.Application.Users;
public sealed record CreateUserRequest(UserDto user);
public sealed record UpdateUserStatusRequest(Guid UserId, bool IsActive);
public sealed record UpdateUserRequest(Guid UserId, UserDto userDto);
