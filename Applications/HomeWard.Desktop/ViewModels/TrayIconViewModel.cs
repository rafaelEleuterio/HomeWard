using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeWard.Desktop.Domain.Enums;
using HomeWard.Desktop.Infrastructure.Client.AuthApiClient;
using HomeWard.Desktop.Infrastructure.Services.Navigation.NavigationService;
using HomeWard.Desktop.Infrastructure.Services.UserService;
using HomeWard.Desktop.Infrastructure.Services.WorkSessionTimer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;

namespace HomeWard.Desktop.ViewModels;

public partial class TrayIconViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IWorkSessionTimer _timer;
    private readonly IUserService _userService;
    public TrayIconViewModel(INavigationService navigationService, IWorkSessionTimer timer, IUserService userService)
    {
        _navigationService = navigationService;
        _timer = timer;
        _userService = userService;

        _userService.UserChanged += UserChanged;
    }

    private void UserChanged(object? sender, EventArgs e)
    {
        NotifyCommands();
    }

    private bool CanStart => _userService.User != null
        && (_timer.State == SessionState.NotStarted || _timer.State == SessionState.Finished);
    private bool CanPause => _userService.User != null
        && _timer.State != SessionState.NotStarted
        && (_timer.State == SessionState.Working || _timer.State == SessionState.Resting);
    private bool CanResume => _userService.User != null
        && _timer.State != SessionState.NotStarted
        && (_timer.State == SessionState.Paused || _timer.State == SessionState.Resting);
    private bool CanStop => _userService.User != null
        && _timer.State != SessionState.Finished
        && _timer.State != SessionState.NotStarted;
    private bool CanRest => _userService.User != null
        && _timer.State != SessionState.Resting
        && _timer.State != SessionState.Finished
        && _timer.State != SessionState.NotStarted;


    [RelayCommand(CanExecute = nameof(CanStart))]
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


    [RelayCommand]
    public void ShowTimer()
    {
        if (_userService.User == null)
        {
            _navigationService.NavigateTo<LoginWindowViewModel>();
        }
        else
        {
            _navigationService.NavigateTo<MainWindowViewModel>();
        }
    }


    [RelayCommand]
    public void Exit()
    {
        _timer.ChangeState(SessionState.Finished);
        App.Current.Shutdown();
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
