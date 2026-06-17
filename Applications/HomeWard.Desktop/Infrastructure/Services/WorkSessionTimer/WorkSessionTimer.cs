using HomeWard.Domain.Entities;
using HomeWard.Domain.Enums;
using System.Windows.Threading;

namespace HomeWard.Desktop.Infrastructure.Services.WorkSessionTimer;

public sealed class WorkSessionTimer : IWorkSessionTimer
{
    private readonly DispatcherTimer _timer;
    private DateTime? _stateStartedAt;

    private readonly WorkSession _session;

    private DateTime _lastTick;

    public event Action? Updated;
    public event Action<SessionTransition>? TransitionAdded;
    public event Action? StateChanged;

    public WorkSessionTimer()
    {
        _session = new WorkSession
        {
            State = SessionState.NotStarted
        };

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(333)
        };

        _timer.Tick += OnTick;
    }

    public SessionState State => _session.State;
    public TimeSpan BillableTime => _session.BillableTime;
    public TimeSpan WorkedTime => _session.WorkedTime;
    public TimeSpan RestedTime => _session.RestedTime;
    public TimeSpan PausedTime => _session.PausedTime;

    public void ChangeState(SessionState state)
    {
        if (_session.State == state)
            return;

        var now = DateTime.UtcNow;

        if (state == SessionState.Finished)
        {
            _timer.Stop();
        }
        else if (!_timer.IsEnabled)
        {
            _lastTick = now;
            _timer.Start();
        }

        if (_stateStartedAt != null)
        {
            var transition = new SessionTransition
            {
                FromState = _session.State,
                ToState = state,
                Timestamp = now,
                TimeElapsed = now - _stateStartedAt.Value
            };

            _session.History.Add(transition);

            TransitionAdded?.Invoke(transition);
        }

        _session.State = state;
        _stateStartedAt = now;

        Updated?.Invoke();
        StateChanged?.Invoke();
    }

    private void OnTick(object? sender, EventArgs e)
    {
        var now = DateTime.UtcNow;

        var elapsed = now - _lastTick;

        _lastTick = now;

        switch (_session.State)
        {
            case SessionState.Working:
                _session.BillableTime += elapsed;
                _session.WorkedTime += elapsed;
                break;

            case SessionState.Resting:
                _session.BillableTime += elapsed;
                _session.RestedTime += elapsed;
                break;

            case SessionState.Paused:
                _session.PausedTime += elapsed;
                break;
        }

        Updated?.Invoke();
    }
}