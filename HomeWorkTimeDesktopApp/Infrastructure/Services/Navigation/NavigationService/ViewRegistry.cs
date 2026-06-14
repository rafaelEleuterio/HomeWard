using System.Windows;

namespace HomeWorkTimeDesktopApp.Infrastructure.Services.Navigation.NavigationService;

public sealed class ViewRegistry
{
    private readonly Dictionary<Type, Type> _viewMappings = [];

    public void Register<TViewModel, TWindow>()
        where TWindow : Window
    {
        _viewMappings[typeof(TViewModel)] = typeof(TWindow);
    }

    public Type GetWindowType(Type viewModelType)
    {
        if (!_viewMappings.TryGetValue(
                viewModelType,
                out var windowType))
        {
            throw new InvalidOperationException(
                $"No window registered for {viewModelType.Name}");
        }

        return windowType;
    }
}
