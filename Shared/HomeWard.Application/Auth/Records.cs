using HomeWard.Domain.Dtos;

namespace HomeWard.Application.Auth;

public sealed record RegisterUserRequest(UserDto User, string Password);
public sealed record RegisterUserResponse(UserDto User);
public sealed record LoginResponse(UserDto User);
public sealed record LoginRequest(string Username, string Password);
public sealed record LoginResult(bool Success, UserDto? User, string ErrorMessage = null);