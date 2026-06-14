using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeWardDesktopApp.Infrastructure.Services.Navigation.WindowService;
using System.Diagnostics;
using System.Windows;

namespace HomeWardDesktopApp.ViewModels;

public partial class TitleBarViewModel : ObservableObject, IWindowAware
{
    private bool _isDebbugMode = Debugger.IsAttached;

    [ObservableProperty]
    private bool closeVisible = true;
    [ObservableProperty]
    private bool minimizeVisible = true;
    [ObservableProperty]
    private bool maximizeVisible = false;
    [ObservableProperty]
    private bool settingsVisible = false;

    public string MainTitle { get; set; }
    public Window? Window { get; set; }

    public TitleBarViewModel(string mainTitle)
    {
        MainTitle = _isDebbugMode ? $"{mainTitle} (Debug)" : mainTitle;
    }

    [RelayCommand]
    private void Close()
    {
        // For now, since we don't have a login and a tray icon, we will just minimize
        // When closing the app, the timer should still run in the background, and the user should be able to open the app via IconTray
        Window?.WindowState = WindowState.Minimized;
    }

    [RelayCommand]
    private void Minimize()
    {
        if (Window is not null)
        {
            Window.WindowState = WindowState.Minimized;
        }
    }

    [RelayCommand]
    private void Maximize()
    {
        if (Window is null)
            return;

        Window.WindowState = Window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    }
}
