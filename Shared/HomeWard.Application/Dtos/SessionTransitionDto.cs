using HomeWard.Domain.Enums;

namespace HomeWard.Application.Dtos;

public sealed record SessionTransitionDto(
    Guid SessionTransitionId,
    DateTime Timestamp,
    SessionState FromState,
    SessionState ToState,
    TimeSpan TimeElapsed,
    string Justification);