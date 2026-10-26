
using HomeWard.Application.Dtos;
using HomeWard.Domain.Enums;

namespace HomeWard.Application.Users;
public sealed record CreateUserRequest(UserDto userDto);
public sealed record UpdateUserRequest(Guid UserId, string Username, 
    string FullName, string FirstLastName, string Email, bool IsActive, Role Role);
public sealed record UpdatePasswordRequest(Guid UserId, string OldPassword, string NewPassword);

public sealed record UpdateUserStatusRequest(Guid UserId, bool IsActive);
