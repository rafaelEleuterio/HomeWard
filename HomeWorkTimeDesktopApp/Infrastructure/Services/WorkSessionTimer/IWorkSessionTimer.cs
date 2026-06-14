using HomeWardDesktopApp.Domain.Enums;
using HomeWardDesktopApp.Domain.Models;

namespace HomeWardDesktopApp.Infrastructure.Services.WorkSessionTimer;

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
