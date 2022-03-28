
namespace Controls;

internal sealed class DoubleToGridLengthCOnverter : BaseValueConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is Double d ? new GridLength(d) : new GridLength(0);

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is GridLength gl ? gl.Value : 0;
    }
}
