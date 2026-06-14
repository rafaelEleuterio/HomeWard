using HomeWard.Desktop.ViewModels;

namespace HomeWard.Desktop.Domain.Models;

public interface ITitleBarAware
{
    TitleBarViewModel TitleBar { get; }
}
