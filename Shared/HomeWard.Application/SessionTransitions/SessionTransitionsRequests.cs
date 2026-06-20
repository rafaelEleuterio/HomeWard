using HomeWard.Domain.Enums;

namespace HomeWard.Application.SessionTransitions;

public sealed record AddTransitionRequest(SessionState FromState, SessionState ToState, DateTime Timestamp, TimeSpan TimeElapsed);
