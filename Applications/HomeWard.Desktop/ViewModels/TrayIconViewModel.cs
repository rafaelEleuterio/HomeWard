using CommunityToolkit.Mvvm.Input;
using HomeWard.Desktop.Infrastructure.Services.Navigation.NavigationService;
using HomeWard.Desktop.Infrastructure.Services.UserService;
using HomeWard.Desktop.Infrastructure.Services.WorkSessionSync;
using HomeWard.Desktop.Infrastructure.Services.WorkSessionTimer;
using HomeWard.Domain.Enums;

namespace HomeWard.Desktop.ViewModels;

public partial class TrayIconViewModel : TimerControlViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IUserService _userService;
    private readonly IWorkSessionSyncService _workSessionSyncService;

    public TrayIconViewModel(INavigationService navigationService, 
        IWorkSessionTimer timer, 
        IUserService userService, 
        IWorkSessionSyncService workSessionSyncService)
        : base(timer)
    {
        _navigationService = navigationService;
        _userService = userService;
        _workSessionSyncService = workSessionSyncService;

        _userService.UserChanged += UserChanged;
    }

    private void UserChanged(object? sender, EventArgs e) => NotifyCommands();

    protected override bool CanStart => _userService.User != null && base.CanStart;
    protected override bool CanPause => _userService.User != null && base.CanPause;
    protected override bool CanResume => _userService.User != null && base.CanResume;
    protected override bool CanStop => _userService.User != null && base.CanStop;
    protected override bool CanRest => _userService.User != null && base.CanRest;

    [RelayCommand]
    public void ShowTimer()
    {
        if (_userService.User == null)
            _navigationService.NavigateTo<LoginWindowViewModel>();
        else
            _navigationService.NavigateTo<MainWindowViewModel>();
    }

    [RelayCommand]
    public void Exit()
    {
        _workSessionSyncService.Stop();
        Timer.ChangeState(SessionState.Finished);
        App.Current.Shutdown();
    }
}