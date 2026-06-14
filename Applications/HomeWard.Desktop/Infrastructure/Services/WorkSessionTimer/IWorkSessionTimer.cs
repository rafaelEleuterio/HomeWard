using HomeWard.Desktop.Domain.Enums;
using HomeWard.Desktop.Domain.Models;

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
