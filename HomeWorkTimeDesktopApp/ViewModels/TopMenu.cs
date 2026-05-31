using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;

namespace HomeWorkTimeDesktopApp.ViewModels;

public partial class TopMenu : ObservableObject
{
    private bool _isDebbugMode = Debugger.IsAttached;

    [ObservableProperty]
    private bool closeVisible = true;
    [ObservableProperty]
    private bool minimizeVisible = true;
    [ObservableProperty]
    private bool maximizeVisible = true;
    [ObservableProperty]
    private bool configVisible = false;

    public TopMenu()
    {
         
    }
}
