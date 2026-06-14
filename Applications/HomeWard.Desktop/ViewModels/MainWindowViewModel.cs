using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeWard.Desktop.Domain.Enums;
using HomeWard.Desktop.Domain.Models;
using HomeWard.Desktop.Infrastructure.Handlers;
using HomeWard.Desktop.Infrastructure.Services.WorkSessionTimer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Text;
using System.Timers;

namespace HomeWard.Desktop.ViewModels;

public partial class MainWindowViewModel : TimerControlViewModelBase, ITitleBarAware
{
    private readonly IWorkSessionTimer _timer;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalWorkedTimeText))]
    private TimeSpan workedTime;

    public ObservableCollection<SessionTransition> History { get; } = [];
    public string TotalWorkedTimeText => $"{WorkedTime.Hours:00}:{WorkedTime.Minutes:00}:{WorkedTime.Seconds:00}";
    public TitleBarViewModel TitleBar { get; }

    public MainWindowViewModel(IWorkSessionTimer timer) : base(timer)
    {
        _timer = timer;
        _timer.Updated += UpdateValues;
        _timer.TransitionAdded += OnTransitionAdded;

        TitleBar = new TitleBarViewModel("HomeWard");
    }

    private void UpdateValues() => WorkedTime = _timer.BillableTime;

    private void OnTransitionAdded(SessionTransition transition) => History.Add(transition);
}
