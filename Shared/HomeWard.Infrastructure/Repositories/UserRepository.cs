using HomeWard.Application.Repositories;
using HomeWard.Domain.Dtos;
using HomeWard.Domain.Entities;
using HomeWard.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HomeWard.Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly HomeWardDbContext _db;

    public UserRepository(HomeWardDbContext db)
    {
        _db = db;
    }

    public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return _db.Users.FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
    }
   
    public async Task<User> CreateAsync(UserDto userDto, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Id = new Guid(),
            FullName = userDto.FullName,
            FirstLastName = userDto.FirstLastName,
            Username = userDto.Username,
            Email = userDto.Email,
            IsActive = true,
            CreatedAt = DateTime.Now,
        };

        await _db.Users.AddAsync(user, cancellationToken);

        await _db.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task SetActiveStatusAsync(Guid userId, bool isActive, CancellationToken cancellationToken)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        if (user is not null)
        {
            user?.IsActive = isActive;
            await _db.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<List<User>?> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Users.ToListAsync(cancellationToken);
    }

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return _db.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await _db.Users.AddAsync(user, cancellationToken);

        await _db.SaveChangesAsync(cancellationToken);
    }
}