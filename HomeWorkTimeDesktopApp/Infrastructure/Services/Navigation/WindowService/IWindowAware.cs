using System.Windows;

namespace HomeWardDesktopApp.Infrastructure.Services.Navigation.WindowService;

public interface IWindowAware
{
    Window? Window { get; set; }
}
