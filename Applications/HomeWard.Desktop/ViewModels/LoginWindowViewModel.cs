using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeWard.Application.Auth;
using HomeWard.Desktop.Domain.Models;
using HomeWard.Desktop.Infrastructure.Client.AuthApiClient;
using HomeWard.Desktop.Infrastructure.Services.Navigation.NavigationService;
using HomeWard.Desktop.Infrastructure.Services.UserService;
using System.Security;
using System.Windows.Controls;

namespace HomeWard.Desktop.ViewModels;

public partial class LoginWindowViewModel : ObservableObject, ITitleBarAware
{
    private readonly IAuthApiClient _authApiClient;
    private readonly INavigationService _navigationService;
    private readonly IUserService _userService;

    [ObservableProperty]
    private string username = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private bool isBusy;

    public TitleBarViewModel TitleBar { get; }

    public LoginWindowViewModel(IAuthApiClient authApiClient, 
        INavigationService navigationService, 
        IUserService userService)
    {
        _authApiClient = authApiClient;
        _navigationService = navigationService;
        _userService = userService;

        TitleBar = new TitleBarViewModel("Login");
        TitleBar.MaximizeVisible = false;
        TitleBar.MinimizeVisible = false;
    }


    [RelayCommand]
    private async Task LoginAsync(object parameter)
    {
        IsBusy = true;

        ErrorMessage = string.Empty;

        try
        {
            if (parameter is PasswordBox passwordBox)
            {   
                var result = await _authApiClient.LoginAsync(Username, passwordBox.Password);

                if (!result.Success)
                {
                    ErrorMessage = result.ErrorMessage ?? "Login failed.";

                    return;
                }

                _userService.SetUser(result.User, result.Token);
            }

            _navigationService.NavigateTo<MainWindowViewModel>();
            _navigationService.Close<LoginWindowViewModel>();
        }
        catch(Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsBusy = false;
        }
    }
}
