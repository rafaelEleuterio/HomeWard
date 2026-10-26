using HomeWard.Domain.Enums;

namespace HomeWard.Application.Dtos;

public class SessionTransitionDto
{
    public Guid Id { get; set; }
    public Guid WorkSessionId { get; set; }
    public SessionState FromState { get; set; }
    public SessionState ToState { get; set; }
    public DateTime Timestamp { get; set; }
    public TimeSpan TimeElapsed { get; set; }
    public List<DocumentDto> Documents { get; set; } = [];

    public SessionTransitionDto() { }
    public SessionTransitionDto(Guid id, Guid workSessionId, SessionState fromState, SessionState toState,
        DateTime timestamp, TimeSpan timeElapsed)
    {
        Id = id;
        WorkSessionId = workSessionId;
        FromState = fromState;
        ToState = toState;
        Timestamp = timestamp;
        TimeElapsed = timeElapsed;
    }
    public SessionTransitionDto(Guid id, Guid workSessionId, SessionState fromState, SessionState toState,
        DateTime timestamp, TimeSpan timeElapsed, List<DocumentDto> documents)
        : this(id, workSessionId, fromState, toState, timestamp, timeElapsed)
    {
        Documents = documents;
    }
}