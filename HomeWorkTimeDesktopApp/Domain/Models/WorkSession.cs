using HomeWorkTimeDesktopApp.Domain.Enums;

namespace HomeWorkTimeDesktopApp.Domain.Models;

public sealed class WorkSession
{
    public SessionState State { get; set; }
    public TimeSpan BillableTime { get; set; }
    public TimeSpan WorkedTime { get; set; }
    public TimeSpan RestedTime { get; set; }
    public TimeSpan PausedTime { get; set; }
    public List<SessionTransition> History { get; } = [];
}