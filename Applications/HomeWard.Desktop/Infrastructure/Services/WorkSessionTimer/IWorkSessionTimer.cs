using HomeWard.Domain.Entities;
using HomeWard.Domain.Enums;

namespace HomeWard.Desktop.Infrastructure.Services.WorkSessionTimer;

public interface IWorkSessionTimer
{
    SessionState State { get; }
    TimeSpan BillableTime { get; }
    TimeSpan WorkedTime { get; }
    TimeSpan RestedTime { get; }
    TimeSpan PausedTime { get; }
    
    event Action? Updated;
    event Action<SessionTransition>? TransitionAdded;
    event Action? StateChanged;
    void ChangeState(SessionState state); 
}
