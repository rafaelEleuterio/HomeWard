using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeWorkTimeDesktopApp.Domain.Enums;
using HomeWorkTimeDesktopApp.Domain.Models;
using HomeWorkTimeDesktopApp.Infrastructure.Handlers;
using HomeWorkTimeDesktopApp.Infrastructure.Services.WorkSessionTimer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Text;
using System.Timers;

namespace HomeWorkTimeDesktopApp.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IWorkSessionTimer _timer;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalWorkedTimeText))]
    private TimeSpan workedTime;
    public ObservableCollection<SessionTransition> History { get; } = [];
    public string MainTitle { get; set; }
    public string TotalWorkedTimeText { get { return $"{WorkedTime.Hours.ToString("00")}:{WorkedTime.Minutes.ToString("00")}:{WorkedTime.Seconds.ToString("00")}"; } }


    public MainWindowViewModel(IWorkSessionTimer timer)
    {
        MainTitle = Debugger.IsAttached ? "HomeWorkTimeDesktopApp (Debug)" : "HomeWorkTimeDesktopApp";
        _timer = timer;

        _timer.Updated += UpdateValues;
        _timer.TransitionAdded += OnTransitionAdded;
    }

    private void UpdateValues()
    {
        WorkedTime = _timer.BillableTime;
    }

    private void OnTransitionAdded(SessionTransition transition)
    {
        History.Add(transition);
    }

    private bool CanStart => _timer.State == SessionState.NotStarted || _timer.State == SessionState.Finished;
    private bool CanPause => (_timer.State == SessionState.Working || _timer.State == SessionState.Resting) && _timer.State != SessionState.NotStarted;
    private bool CanResume => (_timer.State == SessionState.Paused || _timer.State == SessionState.Resting) && _timer.State != SessionState.NotStarted;
    private bool CanStop => _timer.State != SessionState.Finished && _timer.State != SessionState.NotStarted;
    private bool CanRest => _timer.State != SessionState.Resting && _timer.State != SessionState.Finished && _timer.State != SessionState.NotStarted;


    [RelayCommand(CanExecute =nameof(CanStart))]
    public void Start()
    {
        _timer.ChangeState(SessionState.Working);

        NotifyCommands();
    }

    [RelayCommand(CanExecute = nameof(CanPause))]
    public void Pause()
    {
        _timer.ChangeState(SessionState.Paused);

        NotifyCommands();
    }

    [RelayCommand(CanExecute = nameof(CanResume))]
    public void Resume()
    {
        _timer.ChangeState(SessionState.Working);

        NotifyCommands();
    }

    [RelayCommand(CanExecute = nameof(CanStop))]
    public void Stop()
    {
        _timer.ChangeState(SessionState.Finished);

        NotifyCommands();
    }

    [RelayCommand(CanExecute = nameof(CanRest))]
    public void Rest()
    {
        _timer.ChangeState(SessionState.Resting);

        NotifyCommands();
    }

    private void NotifyCommands()
    {
        StartCommand.NotifyCanExecuteChanged();
        PauseCommand.NotifyCanExecuteChanged();
        ResumeCommand.NotifyCanExecuteChanged();
        RestCommand.NotifyCanExecuteChanged();
        StopCommand.NotifyCanExecuteChanged();
    }
}
