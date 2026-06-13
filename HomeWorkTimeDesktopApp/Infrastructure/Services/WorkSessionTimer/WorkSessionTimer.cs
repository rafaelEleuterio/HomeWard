using HomeWorkTimeDesktopApp.Domain.Enums;
using HomeWorkTimeDesktopApp.Domain.Models;
using HomeWorkTimeDesktopApp.Infrastructure.Handlers;
using System.Windows.Threading;

namespace HomeWorkTimeDesktopApp.Infrastructure.Services.WorkSessionTimer;

public sealed class WorkSessionTimer : IWorkSessionTimer
{
    private readonly DispatcherTimer _timer;

    private readonly WorkSession _session;

    private DateTime _lastTick;

    public event Action? Updated;

    public WorkSessionTimer()
    {
        _session = new WorkSession
        {
            State = SessionState.NotStarted
        };

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };

        _timer.Tick += OnTick;
    }

    public SessionState State => _session.State;

    public TimeSpan WorkedTime => _session.WorkedTime;

    public TimeSpan RestTime => _session.RestTime;

    public TimeSpan PausedTime => _session.PausedTime;

    public void ChangeState(SessionState state)
    {
        if (_session.State == state)
            return;

        if (state == SessionState.Finished)
        {
            _timer.Stop();
        }
        else if (!_timer.IsEnabled)
        {
            _lastTick = DateTime.UtcNow;
            _timer.Start();
        }

        _session.State = state;

        Updated?.Invoke();
    }

    private void OnTick(object? sender, EventArgs e)
    {
        var now = DateTime.UtcNow;

        var elapsed = now - _lastTick;

        _lastTick = now;

        switch (_session.State)
        {
            case SessionState.Working:
                _session.WorkedTime += elapsed;
                break;

            case SessionState.Resting:
                _session.RestTime += elapsed;
                break;

            case SessionState.Paused:
                _session.PausedTime += elapsed;
                break;
        }

        Updated?.Invoke();
    }
}