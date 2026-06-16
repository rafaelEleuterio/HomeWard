using HomeWard.Desktop.ViewModels;
using System.Windows;

namespace HomeWard.Desktop.Views;
/// <summary>
/// Lógica interna para LoginWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
    public LoginWindow(LoginWindowViewModel vm)
    {
        InitializeComponent();

        DataContext = vm;
    }
}
