using HomeWard.Desktop.Infrastructure.Services.Navigation.ActivatorWindow;
using HomeWard.Desktop.Infrastructure.Services.Navigation.WindowService;

namespace HomeWard.Desktop.Infrastructure.Services.Navigation.NavigationService;

public sealed class NavigationService : INavigationService
{
    private readonly IWindowService _windowService;

    public NavigationService(
        IWindowService windowService)
    {
        _windowService = windowService;
    }

    public void Close<TViewModel>() where TViewModel : class
    {
        _windowService.Close<TViewModel>();
    }

    public void NavigateTo<TViewModel>() where TViewModel : class
    {
        _windowService.Show<TViewModel>();
    }

    public bool? NavigateToDialog<TViewModel>() where TViewModel : class
    {
        return _windowService.ShowDialog<TViewModel>();
    }

    public void Replace<TCurrentViewModel, TTargetViewModel>()
        where TCurrentViewModel : class
        where TTargetViewModel : class
    {
        _windowService.Close<TCurrentViewModel>();
        _windowService.Show<TTargetViewModel>();
    }
}
