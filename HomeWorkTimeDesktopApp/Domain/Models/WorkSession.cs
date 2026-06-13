using HomeWorkTimeDesktopApp.Domain.Enums;

namespace HomeWorkTimeDesktopApp.Domain.Models;

public sealed class WorkSession
{
    public SessionState State { get; set; }

    public TimeSpan WorkedTime { get; set; }

    public TimeSpan RestTime { get; set; }

    public TimeSpan PausedTime { get; set; }
}