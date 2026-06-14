namespace HomeWard.Desktop.Domain.Records;

public sealed record LoginRequest(string Email, string Password);
public sealed record LoginResponse(Guid UserId, string Email);
public sealed record LoginResult(bool Success, string? ErrorMessage, LoginResponse? User);