using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;

namespace HomeWorkTimeDesktopApp.ViewModels;

public partial class TitleBarViewModel : ObservableObject
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

    public string MainTitle { get; set; }
    
    public TitleBarViewModel(string mainTitle)
    {
        MainTitle = Debugger.IsAttached ? $"{mainTitle} (Debug)" : mainTitle;
    }
}
