using HomeWorkTimeDesktopApp.Domain.Enums;

namespace HomeWorkTimeDesktopApp.Infrastructure.Services.WorkSessionTimer;

public interface IWorkSessionTimer
{
    SessionState State { get; }

    TimeSpan BillableTime { get; }
    TimeSpan WorkedTime { get; }

    TimeSpan RestedTime { get; }

    TimeSpan PausedTime { get; }

    event Action? Updated;

    void ChangeState(SessionState state);
}
