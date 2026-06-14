using HomeWorkTimeDesktopApp.Domain.Enums;
using HomeWorkTimeDesktopApp.Domain.Models;

namespace HomeWorkTimeDesktopApp.Infrastructure.Services.WorkSessionTimer;

public interface IWorkSessionTimer
{
    SessionState State { get; }
    TimeSpan BillableTime { get; }
    TimeSpan WorkedTime { get; }
    TimeSpan RestedTime { get; }
    TimeSpan PausedTime { get; }
    
    event Action? Updated;
    event Action<SessionTransition>? TransitionAdded;
    void ChangeState(SessionState state);
}
