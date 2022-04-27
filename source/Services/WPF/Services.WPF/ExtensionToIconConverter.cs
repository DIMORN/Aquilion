
namespace Services.WPF;

public class ExtensionToIconConverter : BaseMultiValueConverter
{
    public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[1] is string s)
        {
            if (values[0] is FileSystemModel m)
            {

                if (Equals(s, Locale.FileSystem_Extension_Directory))
                    return new BitmapImage(new Uri(ImageWork.GetIconFileName(m, int.Parse(parameter.ToString()))));
                else
                if (Equals(s, Locale.FileSystem_Extension_Storage_Default))
                    return new BitmapImage(new Uri(ImageWork.GetIconFileName(m, int.Parse(parameter.ToString()))));
                else return new BitmapImage(new Uri(ImageWork.GetIconFileName(m, int.Parse(parameter.ToString()))));
            }
        }
        return null;
    }
}
