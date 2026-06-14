using HomeWardDesktopApp.Infrastructure.Services.Navigation.NavigationService;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace HomeWardDesktopApp.Infrastructure.Services.Navigation.ActivatorWindow;

public sealed class ActivatorWindow : IActivatorWindow
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ViewRegistry _registry;

    public ActivatorWindow(IServiceProvider serviceProvider, ViewRegistry registry)
    {
        _serviceProvider = serviceProvider;
        _registry = registry;
    }

    public Window CreateWindow<TViewModel>()
        where TViewModel : class
    {
        return CreateWindow(typeof(TViewModel));
    }

    public Window CreateWindow(Type viewModelType)
    {
        var vm = _serviceProvider.GetRequiredService(viewModelType);

        var windowType = _registry.GetWindowType(viewModelType);

        var window = (Window)_serviceProvider.GetRequiredService(windowType);

        window.DataContext = vm;

        return window;
    }
}
