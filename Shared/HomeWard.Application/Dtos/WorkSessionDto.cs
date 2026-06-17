using HomeWard.Domain.Enums;

namespace HomeWard.Application.Dtos;

public sealed record WorkSessionDto(
    DateTime CreatedAt,
    SessionState State,
    TimeSpan WorkedTime,
    TimeSpan RestedTime,
    TimeSpan BillableTime,
    List<SessionTransitionDto> History);
