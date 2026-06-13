using HomeWorkTimeDesktopApp.Domain.Enums;

namespace HomeWorkTimeDesktopApp.Infrastructure.Services.WorkSessionTimer;

public interface IWorkSessionTimer
{
    SessionState State { get; }

    TimeSpan WorkedTime { get; }

    TimeSpan RestTime { get; }

    TimeSpan PausedTime { get; }

    event Action? Updated;

    void ChangeState(SessionState state);
}
