

namespace Services.WPF
{
    public sealed class ExtensionToIconConverter : BaseMultiValueConverter
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[1] is string s)
            {
                if (values[0] is FileSystemModel m)
                {

                    if (Equals(s, Locale.FileSystem_Extension_Directory))
                        return new BitmapImage(new Uri(ImageWork.Instance.GetIconFileName(m, int.Parse(parameter.ToString()))));
                    else
                    if (Equals(s, Locale.FileSystem_Extension_Storage_Default))
                        return new BitmapImage(new Uri(ImageWork.Instance.GetIconFileName(m, int.Parse(parameter.ToString()))));
                    else return new BitmapImage(new Uri(ImageWork.Instance.GetIconFileName(m, int.Parse(parameter.ToString()))));
                }
            }
            return null;
        }
    }

    public sealed class TaskHeaderToIconConverter : BaseValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value as MenuItemViewModel).Icon != null) return new BitmapImage(new Uri((value as MenuItemViewModel).Icon.ToString()));
            return new BitmapImage(new Uri(ImageWork.Instance.GetTaskIcon(value as MenuItemViewModel, int.Parse(parameter.ToString()))));
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
