using HomeWard.Domain.Enums;

namespace HomeWard.Application.WorkSessions;

public sealed record StartWorkSessionRequest(Guid UserId);

public sealed record GetWorkSessionRequest(Guid UserId);

public sealed record FinishWorkSessionRequest(TimeSpan BillableTime, TimeSpan WorkedTime, TimeSpan RestedTime, TimeSpan PausedTime);