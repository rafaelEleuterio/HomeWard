using H.NotifyIcon;
using HomeWard.Desktop.Infrastructure.Client.AuthApiClient;
using HomeWard.Desktop.Infrastructure.Services.Navigation.ActivatorWindow;
using HomeWard.Desktop.Infrastructure.Services.Navigation.NavigationService;
using HomeWard.Desktop.Infrastructure.Services.Navigation.WindowService;
using HomeWard.Desktop.Infrastructure.Services.UserService;
using HomeWard.Desktop.Infrastructure.Services.WorkSessionTimer;
using HomeWard.Desktop.ViewModels;
using HomeWard.Desktop.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace HomeWard.Desktop;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    private readonly IHost _host;
    private TaskbarIcon _notifyIcon;
    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(ConfigureServices)
            .Build();
    }

    private static void ConfigureServices(
        HostBuilderContext context,
        IServiceCollection services)
    {
        RegisterServices(services);
        RegisterViewModels(services);
        RegisterViews(services);
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();

        var navigationService = _host.Services.GetRequiredService<INavigationService>();
        navigationService.NavigateTo<LoginWindowViewModel>();
        var trayIcon = (H.NotifyIcon.TaskbarIcon)this.FindResource("TrayIconResource");
        trayIcon.ForceCreate();
        trayIcon.DataContext = _host.Services.GetRequiredService<TrayIconViewModel>();
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();

        _host.Dispose();

        base.OnExit(e);
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<IWorkSessionTimer, WorkSessionTimer>();
        services.AddSingleton<ViewRegistry>();
        services.AddSingleton<IActivatorWindow, ActivatorWindow>();
        services.AddSingleton<IWindowService, WindowService>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IUserService, UserService>();
        services.AddHttpClient<IAuthApiClient, AuthApiClient>(client => { client.BaseAddress = new Uri("http://localhost:8080/"); });
    }

    private static void RegisterViewModels(IServiceCollection services)
    {
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<LoginWindowViewModel>();
        services.AddTransient<TrayIconViewModel>();
    }

    private static void RegisterViews(IServiceCollection services)
    {
        services.AddTransient<MainWindow>();
        services.AddTransient<LoginWindow>();

        services.AddSingleton(sp =>
        {
            var registry = new ViewRegistry();

            registry.Register<MainWindowViewModel, MainWindow>();
            registry.Register<LoginWindowViewModel, LoginWindow>();
            registry.Register<SettingsWindowViewModel, SettingsWindow>();

            return registry;
        });
    }
    private void MenuExit_Click(object sender, RoutedEventArgs e)
    {
        _notifyIcon.Dispose();
        Shutdown();
    }
}

