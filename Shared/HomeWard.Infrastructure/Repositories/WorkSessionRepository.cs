using HomeWard.Application.Repositories;
using HomeWard.Domain.Entities;
using HomeWard.Domain.Enums;
using HomeWard.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HomeWard.Infrastructure.Repositories;

public class WorkSessionRepository : IWorkSessionRepository
{
    private readonly HomeWardDbContext _context;

    public WorkSessionRepository(HomeWardDbContext context)
    {
        _context = context;
    }

    public async Task<WorkSession> CreateAsync(WorkSession session, CancellationToken cancellationToken = default)
    {
        session.Id = Guid.NewGuid();
        session.CreatedAt = DateTime.UtcNow;

        await _context.WorkSessions.AddAsync(session, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return session;
    }

    public async Task<WorkSession?> GetActiveByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.WorkSessions
            .Include(s => s.History)
            .Where(s => s.UserId == userId
                && s.State != SessionState.Finished)
            .OrderByDescending(s => s.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<WorkSession?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.WorkSessions
            .Include(s => s.History)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<WorkSession>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.WorkSessions
            .Include(s => s.History)
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<WorkSession> UpdateAsync(WorkSession session, CancellationToken cancellationToken = default)
    {
        _context.WorkSessions.Update(session);
        await _context.SaveChangesAsync(cancellationToken);

        return session;
    }

    public async Task AddTransitionAsync(SessionTransition transition, CancellationToken cancellationToken = default)
    {
        transition.Id = Guid.NewGuid();
        transition.CreatedAt = DateTime.UtcNow;

        await _context.SessionTransitions.AddAsync(transition, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}