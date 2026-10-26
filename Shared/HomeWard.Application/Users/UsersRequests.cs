
using HomeWard.Domain.Enums;

namespace HomeWard.Application.Users;
public sealed record CreateUserRequest(string Username, string Password,
    string FullName, string FirstLastName, string Email, bool IsActive, Role Role);
public sealed record UpdateUserRequest(Guid UserId, string Username, 
    string FullName, string FirstLastName, string Email, bool IsActive, Role Role);
public sealed record UpdatePasswordRequest(Guid UserId, string OldPassword, string NewPassword);