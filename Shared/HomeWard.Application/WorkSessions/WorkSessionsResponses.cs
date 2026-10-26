using HomeWard.Application.SessionTransitions;
using HomeWard.Domain.Entities;
using HomeWard.Domain.Enums;

namespace HomeWard.Application.WorkSessions;

public sealed record WorkSessionResponse(Guid Id, Guid UserId, SessionState State, TimeSpan BillableTime,
    TimeSpan WorkedTime, TimeSpan RestedTime, TimeSpan PausedTime, DateTime CreatedAt, IReadOnlyList<SessionTransitionResponse> History);