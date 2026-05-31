using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Text;

namespace HomeWorkTimeDesktopApp.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private System.Timers.Timer _workedTimer = new System.Timers.Timer(333);
    private System.Timers.Timer _paidPausedTimer = new System.Timers.Timer(333);
    private System.Timers.Timer _unPaidTimer = new System.Timers.Timer(333);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalWorkedTimeText))]
    private TimeSpan totalWorkedTime = TimeSpan.Zero;
    [ObservableProperty]
    private TimeSpan totalPaidPausedTime = TimeSpan.Zero;
    [ObservableProperty]
    private TimeSpan currentlyPaidPausedTime = TimeSpan.Zero;
    [ObservableProperty]
    private TimeSpan totalUnaidPausedTime = TimeSpan.Zero;
    [ObservableProperty]
    private TimeSpan currentlyUnpaidPausedTime = TimeSpan.Zero;
    
    public string MainTitle { get; set; }
    public string TotalWorkedTimeText { get { return $"{TotalWorkedTime.Hours}:{TotalWorkedTime.Minutes}"; } }


    public MainWindowViewModel()
    {
        MainTitle = Debugger.IsAttached ? "HomeWorkTimeDesktopApp (Debug)" : "HomeWorkTimeDesktopApp";
    }
}
