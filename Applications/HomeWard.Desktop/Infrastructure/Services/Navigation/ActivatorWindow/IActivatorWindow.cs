using System.Windows;

namespace HomeWard.Desktop.Infrastructure.Services.Navigation.ActivatorWindow;

public interface IActivatorWindow
{
    Window CreateWindow<TViewModel>() where TViewModel : class;
    Window CreateWindow(Type viewModelType);
}
