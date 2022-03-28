namespace Explorer;

public abstract class BaseValueConverter : MarkupExtension, IValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider) => this;

    public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

    public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

internal sealed class DoubleToGridLengthCOnverter : BaseValueConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is Double d ? new GridLength(d) : new GridLength(0);
}
