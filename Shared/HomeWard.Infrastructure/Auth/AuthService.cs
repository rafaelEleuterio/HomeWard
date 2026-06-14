using HomeWard.Application.Auth;
using HomeWard.Application.Repositories;
using HomeWard.Domain.Dtos;
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
        var user = await _users.GetByUsernameAsync(request.Username, cancellationToken);

        if (user is null)
        {
            return new LoginResult(false, null, "Usuário não encontrado");
        }

        var validPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!validPassword)
        {
            return new LoginResult(false, null, "Usuário não autenticado");
        }

        var userDto = new UserDto()
        {
            Id = user.Id,
            FullName = user.FullName,
            FirstLastName = user.FirstLastName,
            Email = user.Email,
            Username = request.Username
        };
        return new LoginResult(true, userDto, null);
    }

    public async Task<RegisterUserResponse> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var existingUsername = await _users.GetByUsernameAsync(request.User.Username, cancellationToken);

        if (existingUsername is not null)
        {
            throw new InvalidOperationException("Usuário já existente.");
        }

        var existingEmail = await _users.GetByEmailAsync(request.User.Email, cancellationToken);

        if (existingEmail is not null)
        {
            throw new InvalidOperationException("Este email já está sendo utilizado.");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = request.User.Username,
            FullName = request.User.FullName,
            FirstLastName = request.User.FirstLastName,
            Email = request.User.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            CreatedAt = DateTime.UtcNow
        };

        await _users.AddAsync(user, cancellationToken);
        
        var userDto = new UserDto()
        {
            Id = user.Id,
            FullName = user.FullName,
            FirstLastName = user.FirstLastName,
            Email = user.Email,
            Username = request.User.Username
        };

        return new RegisterUserResponse(userDto);
    }
}