
namespace Services.WPF
{
    public sealed class NullToVisibleHiddenConverter : BaseValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = Visibility.Visible;
            if (value == null & parameter.ToString() == "visible") visibility = Visibility.Visible;
            else if (value == null & parameter.ToString() == "hidden") visibility = Visibility.Collapsed;
            return visibility;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
