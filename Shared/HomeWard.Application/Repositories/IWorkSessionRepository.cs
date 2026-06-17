using HomeWard.Domain.Entities;

namespace HomeWard.Application.Repositories;

public interface IWorkSessionRepository
{
    Task<WorkSession> CreateAsync(WorkSession session, CancellationToken cancellationToken = default);
    Task<WorkSession?> GetActiveByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<WorkSession?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<WorkSession>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<WorkSession> UpdateAsync(WorkSession session, CancellationToken cancellationToken = default);
    Task AddTransitionAsync(SessionTransition transition, CancellationToken cancellationToken = default);
}