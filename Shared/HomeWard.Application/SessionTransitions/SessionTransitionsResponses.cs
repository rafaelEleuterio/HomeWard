using HomeWard.Domain.Enums;

namespace HomeWard.Application.SessionTransitions;

public sealed record SessionTransitionResponse(Guid Id, Guid WorkSessionId, SessionState FromState, 
    SessionState ToState, DateTime Timestamp, TimeSpan TimeElapsed);
