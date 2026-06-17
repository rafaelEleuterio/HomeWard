using HomeWard.Domain.Enums;

namespace HomeWard.Application.WorkSessions;

public sealed record StartWorkSessionRequest(Guid UserId);

public sealed record AddTransitionRequest(Guid WorkSessionId, SessionState FromState, SessionState ToState, DateTime Timestamp, TimeSpan TimeElapsed);

public sealed record FinishWorkSessionRequest(Guid WorkSessionId, TimeSpan BillableTime, TimeSpan WorkedTime, TimeSpan RestedTime, TimeSpan PausedTime);