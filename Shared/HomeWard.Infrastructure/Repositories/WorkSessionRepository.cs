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

    public async Task<IReadOnlyList<SessionTransition>> GetTransitionsByWorkSessionIdAsync(Guid workSessionId, CancellationToken cancellationToken = default)
    {
        var transitions = await _context.SessionTransitions
            .Where(t => t.WorkSessionId == workSessionId)
            .ToListAsync(cancellationToken);

        return transitions;
    }

    public async Task<SessionTransition?> GetTransitionByIdAsync(Guid transitionId, CancellationToken cancellationToken = default)
    {
        var transition = await _context.SessionTransitions
            .SingleOrDefaultAsync(t => t.Id == transitionId, cancellationToken);

        return transition;
    }

    public async Task SaveDocumentAsync(Guid transitionId, SessionTransitionDocument document, CancellationToken cancellationToken = default)
    {
        var transition = await _context.SessionTransitions
            .Include(t => t.Documents)
            .SingleOrDefaultAsync(t => t.Id == transitionId, cancellationToken);

        if(transition == null)
            throw new ArgumentNullException("SessionTransition not found!");

        transition.Documents.Add(document);
        
        await _context.SaveChangesAsync();

        //Poderia simplificar para isso
        //document.SessionTransitionId = transitionId;
        //_context.SessionTransitionDocuments.Add(document);
        //await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<SessionTransitionDocument?> GetDocumentByIdAsync(Guid documentId, CancellationToken cancellationToken = default)
    {
        var transition = await _context.SessionTransitions
            .SelectMany(t => t.Documents)
            .SingleOrDefaultAsync(d => d.Id == documentId, cancellationToken);

        return transition;
    }
}