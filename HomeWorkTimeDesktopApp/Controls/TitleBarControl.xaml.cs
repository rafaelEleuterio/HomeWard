using HomeWardDesktopApp.Infrastructure.Services.Navigation.WindowService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HomeWardDesktopApp.Controls;
/// <summary>
/// Interação lógica para TitleBarControl.xam
/// </summary>
public partial class TitleBarControl : UserControl
{
    public TitleBarControl()
    {
        InitializeComponent();

        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is IWindowAware windowAware)
        {
            windowAware.Window = Window.GetWindow(this);
        }
    }

    private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var window = Window.GetWindow(this);

        if (window is null)
            return;

        window.DragMove();
    }
}
