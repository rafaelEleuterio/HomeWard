using HomeWorkTimeDesktopApp.Infrastructure.Services.WorkSessionTimer;
using HomeWorkTimeDesktopApp.ViewModels;
using HomeWorkTimeDesktopApp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;

namespace HomeWorkTimeDesktopApp;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;

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

        var mainWindow =
            _host.Services.GetRequiredService<MainWindow>();

        mainWindow.Show();

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
    }

    private static void RegisterViewModels(IServiceCollection services)
    {
        services.AddTransient<MainWindowViewModel>();
    }

    private static void RegisterViews(IServiceCollection services)
    {
        services.AddTransient<MainWindow>();
    }
}

