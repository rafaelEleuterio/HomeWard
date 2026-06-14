using HomeWard.Application.Auth;
using HomeWard.Application.Repositories;
using HomeWard.Domain.Entities;
using HomeWard.Infrastructure.Repositories;

namespace HomeWard.Infrastructure.Auth;

public sealed class AuthService : IAuthService
{
    private readonly IUserRepository _users;

    public AuthService(IUserRepository users)
    {
        _users = users;
    }

    public async Task<LoginResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _users.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            return new LoginResult(false, null);
        }

        var validPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!validPassword)
        {
            return new LoginResult(false, null, "Usuário não autenticado");
        }

        return new LoginResult(true, new LoginResponse(user.Id, user.Email));
    }

    public async Task<RegisterUserResponse> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var existingUser = await _users.GetByEmailAsync(request.Email, cancellationToken);

        if (existingUser is not null)
        {
            throw new InvalidOperationException("User already exists.");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            CreatedAt = DateTime.UtcNow
        };

        await _users.AddAsync(user, cancellationToken);

        return new RegisterUserResponse(user.Id, user.Email);
    }
}