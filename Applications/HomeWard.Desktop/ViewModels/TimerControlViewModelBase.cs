using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeWard.Desktop.Infrastructure.Services.WorkSessionTimer;
using HomeWard.Domain.Enums;

namespace HomeWard.Desktop.ViewModels;

public abstract partial class TimerControlViewModelBase : ObservableObject
{
    protected readonly IWorkSessionTimer Timer;

    protected TimerControlViewModelBase(IWorkSessionTimer timer)
    {
        Timer = timer;
        Timer.StateChanged += OnTimerStateChanged;
    }

    private void OnTimerStateChanged()
    {
        NotifyCommands();
    }

    protected virtual bool CanStart => Timer.State == SessionState.NotStarted || Timer.State == SessionState.Finished;
    protected virtual bool CanPause => (Timer.State == SessionState.Working || Timer.State == SessionState.Resting) && Timer.State != SessionState.NotStarted;
    protected virtual bool CanResume => (Timer.State == SessionState.Paused || Timer.State == SessionState.Resting) && Timer.State != SessionState.NotStarted;
    protected virtual bool CanStop => Timer.State != SessionState.Finished && Timer.State != SessionState.NotStarted;
    protected virtual bool CanRest => Timer.State != SessionState.Resting && Timer.State != SessionState.Finished && Timer.State != SessionState.NotStarted;

    [RelayCommand(CanExecute = nameof(CanStart))]
    public void Start()
    {
        Timer.ChangeState(SessionState.Working);
    }

    [RelayCommand(CanExecute = nameof(CanPause))]
    public void Pause()
    {
        Timer.ChangeState(SessionState.Paused);
    }

    [RelayCommand(CanExecute = nameof(CanResume))]
    public void Resume()
    {
        Timer.ChangeState(SessionState.Working);
    }

    [RelayCommand(CanExecute = nameof(CanStop))]
    public void Stop()
    {
        Timer.ChangeState(SessionState.Finished);
    }

    [RelayCommand(CanExecute = nameof(CanRest))]
    public void Rest()
    {
        Timer.ChangeState(SessionState.Resting);
    }

    protected virtual void NotifyCommands()
    {
        StartCommand.NotifyCanExecuteChanged();
        PauseCommand.NotifyCanExecuteChanged();
        ResumeCommand.NotifyCanExecuteChanged();
        RestCommand.NotifyCanExecuteChanged();
        StopCommand.NotifyCanExecuteChanged();
    }
}
