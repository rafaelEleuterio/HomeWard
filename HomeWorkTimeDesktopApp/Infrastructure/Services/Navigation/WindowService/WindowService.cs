using HomeWorkTimeDesktopApp.Infrastructure.Services.Navigation.ActivatorWindow;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace HomeWorkTimeDesktopApp.Infrastructure.Services.Navigation.WindowService;

public sealed class WindowService : IWindowService
{
    private readonly IActivatorWindow _activator;

    private readonly Dictionary<Type, Window> _windows = [];

    public WindowService(IActivatorWindow activator)
    {
        _activator = activator;
    }

    public void Show<TViewModel>() where TViewModel : class
    {
        var type = typeof(TViewModel);

        if (_windows.TryGetValue(type, out var existing))
        {
            existing.Activate();
            return;
        }

        var window = _activator.CreateWindow<TViewModel>();

        window.Closed += (_, _) =>
        {
            _windows.Remove(type);
        };

        _windows[type] = window;

        window.Show();
    }

    public bool? ShowDialog<TViewModel>() where TViewModel : class
    {
        var window = _activator.CreateWindow<TViewModel>();

        return window.ShowDialog();
    }

    public bool IsOpen<TViewModel>() where TViewModel : class
    {
        return _windows.ContainsKey(typeof(TViewModel));
    }

    public void Close<TViewModel>() where TViewModel : class
    {
        if (_windows.TryGetValue(typeof(TViewModel), out var window))
        {
            window.Close();
        }
    }

    public void Activate<TViewModel>() where TViewModel : class
    {
        if (_windows.TryGetValue(typeof(TViewModel), out var window))
        {
            window.Activate();
        }
    }

    public void Minimize<TViewModel>() where TViewModel : class
    {
        if (_windows.TryGetValue(typeof(TViewModel), out var window))
        {
            window.WindowState = WindowState.Minimized;
        }
    }

    public void Maximize<TViewModel>() where TViewModel : class
    {
        if (_windows.TryGetValue(typeof(TViewModel), out var window))
        {
            window.WindowState = WindowState.Maximized;
        }
    }

    public void Restore<TViewModel>() where TViewModel : class
    {
        if (_windows.TryGetValue(typeof(TViewModel), out var window))
        {
            window.WindowState = WindowState.Normal;
        }
    }
}
