using HomeWardDesktopApp.ViewModels;

namespace HomeWardDesktopApp.Domain.Models;

public interface ITitleBarAware
{
    TitleBarViewModel TitleBar { get; }
}
