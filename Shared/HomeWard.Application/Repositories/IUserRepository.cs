using HomeWard.Domain.Dtos;
using HomeWard.Domain.Entities;

namespace HomeWard.Application.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(UserDto userDto, CancellationToken cancellationToken);
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task<User> CreateAsync(UserDto userDto, CancellationToken cancellationToken);
    Task SetActiveStatusAsync(Guid userId, bool isActive, CancellationToken cancellationToken);
    Task<List<User>?> GetAllAsync(CancellationToken cancellationToken);

    Task SaveChanges(CancellationToken cancellationToken);
}
