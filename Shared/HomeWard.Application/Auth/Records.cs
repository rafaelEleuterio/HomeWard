namespace HomeWard.Application.Auth;

public sealed record RegisterUserRequest(string Email, string Password);
public sealed record RegisterUserResponse(Guid UserId, string Email);
public sealed record LoginResponse(Guid UserId, string Email);
public sealed record LoginRequest(string Email, string Password);
public sealed record LoginResult(bool Success, LoginResponse? Response, string ErrorMessage = null);