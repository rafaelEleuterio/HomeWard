using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Text;
using HomeWorkTimeDesktopApp.Infrastructure.Services.WorkSessionTimer;
using HomeWorkTimeDesktopApp.Infrastructure.Handlers;
using CommunityToolkit.Mvvm.Input;
using HomeWorkTimeDesktopApp.Domain.Enums;

namespace HomeWorkTimeDesktopApp.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IWorkSessionTimer _timer;

    [ObservableProperty]
    private TimeSpan workedTime;
    public string MainTitle { get; set; }
    public string TotalWorkedTimeText { get { return $"{WorkedTime.Hours}:{WorkedTime.Minutes}"; } }


    public MainWindowViewModel(IWorkSessionTimer timer)
    {
        MainTitle = Debugger.IsAttached ? "HomeWorkTimeDesktopApp (Debug)" : "HomeWorkTimeDesktopApp";
        _timer = timer;

        _timer.Updated += UpdateValues;
    }
    private void UpdateValues()
    {
        WorkedTime = _timer.WorkedTime;
    }

    [RelayCommand]
    public void Start()
    {
        _timer.ChangeState(SessionState.Paused);
    }
}
