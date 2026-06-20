using HomeWard.Domain.Enums;

namespace HomeWard.Application.Dtos;

public class WorkSessionDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public SessionState State { get; set; }

    public TimeSpan BillableTime { get; set; }
    public TimeSpan WorkedTime { get; set; }
    public TimeSpan RestedTime { get; set; }
    public TimeSpan PausedTime { get; set; }

    public List<SessionTransitionDto> History { get; } = [];

    public WorkSessionDto() { }

    public WorkSessionDto(Guid id, Guid userId, SessionState state, TimeSpan billableTime,
        TimeSpan workedTime, TimeSpan restedTime, TimeSpan pausedTime)
    {
        Id = id;
        UserId = userId;
        State = state;
        BillableTime = billableTime;
        WorkedTime = workedTime;
        RestedTime = restedTime;
        PausedTime = pausedTime;
    }

    public WorkSessionDto(Guid id, Guid userId, SessionState state, TimeSpan billableTime,
        TimeSpan workedTime, TimeSpan restedTime, TimeSpan pausedTime, List<SessionTransitionDto> history)
        : this(id, userId, state, billableTime, workedTime, restedTime, pausedTime)
    {
        History = history;
    }
}