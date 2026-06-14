using HomeWard.Application.Repositories;
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