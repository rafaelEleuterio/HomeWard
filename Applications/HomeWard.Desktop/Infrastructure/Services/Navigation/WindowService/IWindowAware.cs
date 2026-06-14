using System.Windows;

namespace HomeWard.Desktop.Infrastructure.Services.Navigation.WindowService;

public interface IWindowAware
{
    Window? Window { get; set; }
}
