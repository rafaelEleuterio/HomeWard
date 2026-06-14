using HomeWorkTimeDesktopApp.ViewModels;

namespace HomeWorkTimeDesktopApp.Domain.Models;

public interface ITitleBarAware
{
    TitleBarViewModel TitleBar { get; }
}
