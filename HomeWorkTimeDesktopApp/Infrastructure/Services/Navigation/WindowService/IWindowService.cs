namespace HomeWard.Desktop.Infrastructure.Services.Navigation.WindowService;

public interface IWindowService
{
    void Show<TViewModel>() where TViewModel : class;
    bool? ShowDialog<TViewModel>() where TViewModel : class;
    void Close<TViewModel>() where TViewModel : class;
    void Minimize<TViewModel>() where TViewModel : class;
    void Maximize<TViewModel>() where TViewModel : class;
    void Restore<TViewModel>() where TViewModel : class;
    void Activate<TViewModel>() where TViewModel : class;
    bool IsOpen<TViewModel>() where TViewModel : class;
}
