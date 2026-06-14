using System.Windows;

namespace HomeWorkTimeDesktopApp.Infrastructure.Services.Navigation.WindowService;

public interface IWindowAware
{
    Window? Window { get; set; }
}
