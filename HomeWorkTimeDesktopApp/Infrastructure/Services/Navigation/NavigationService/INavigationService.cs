namespace HomeWorkTimeDesktopApp.Infrastructure.Services.Navigation.NavigationService;

public interface INavigationService
{
    void NavigateTo<TViewModel>() where TViewModel : class;
    bool? NavigateToDialog<TViewModel>() where TViewModel : class;
    void Close<TViewModel>() where TViewModel : class;
    void Replace<TCurrentViewModel, TTargetViewModel>() 
        where TCurrentViewModel : class
        where TTargetViewModel : class;
}
