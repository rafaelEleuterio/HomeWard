using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HomeWard.Desktop.Infrastructure.Converters;

public class BooleanToVisibilityConverter : IValueConverter
{
    public bool Inverted { get; set; } = false;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool boolValue = value is bool b && b;

        // Se Inverted for true, invertemos a lógica do boolean
        if (Inverted) boolValue = !boolValue;

        return boolValue ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is Visibility v && v == Visibility.Visible;
    }
}
