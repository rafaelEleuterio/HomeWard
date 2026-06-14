using System.Windows;

namespace HomeWorkTimeDesktopApp.Infrastructure.Services.Navigation.ActivatorWindow;

public interface IActivatorWindow
{
    Window CreateWindow<TViewModel>() where TViewModel : class;
    Window CreateWindow(Type viewModelType);
}
