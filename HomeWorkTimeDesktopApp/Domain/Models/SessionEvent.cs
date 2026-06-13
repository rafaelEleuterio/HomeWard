using HomeWorkTimeDesktopApp.Domain.Enums;

namespace HomeWorkTimeDesktopApp.Domain.Models;

public class SessionEvent
{
    public DateTime Timestamp { get; init; }
    public SessionEventType Type { get; init; }
}
