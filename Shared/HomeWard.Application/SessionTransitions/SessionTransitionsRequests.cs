using HomeWard.Domain.Enums;

namespace HomeWard.Application.SessionTransitions;

public sealed record AddTransitionRequest(Guid WorkSessionId, SessionState FromState, SessionState ToState, DateTime Timestamp, TimeSpan TimeElapsed);
