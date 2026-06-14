using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeWard.Desktop.Domain.Models;
using HomeWard.Desktop.Infrastructure.Client.AuthApiClient;
using HomeWard.Desktop.Infrastructure.Services.Navigation.NavigationService;

namespace HomeWard.Desktop.ViewModels;

public partial class LoginWindowViewModel : ObservableObject, ITitleBarAware
{
    private readonly IAuthApiClient _authApiClient;

    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private string email = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private bool isBusy;

    public TitleBarViewModel TitleBar { get; }

    public LoginWindowViewModel(IAuthApiClient authApiClient, INavigationService navigationService)
    {
        _authApiClient = authApiClient;
        _navigationService = navigationService;

        TitleBar = new TitleBarViewModel("Login");
        TitleBar.MaximizeVisible = false;
        TitleBar.MinimizeVisible = false;
    }


    [RelayCommand]
    private async Task LoginAsync()
    {
        IsBusy = true;

        ErrorMessage = string.Empty;

        try
        {
            var result = await _authApiClient.LoginAsync(Email, Password);

            if (!result.Success)
            {
                ErrorMessage = result.ErrorMessage ?? "Login failed.";

                return;
            }

            _navigationService.NavigateTo<MainWindowViewModel>();
        }
        finally
        {
            IsBusy = false;
        }
    }
}
