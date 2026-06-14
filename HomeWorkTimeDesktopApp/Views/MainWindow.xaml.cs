using HomeWorkTimeDesktopApp.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace HomeWorkTimeDesktopApp.Views;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }

    private void MyScrollViewer_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs e)
    {
        // Only scroll down if the content height actually grew
        if (e.ExtentHeightChange > 0)
        {
            HistoryScrollViewer.ScrollToBottom();
        }
    }
}