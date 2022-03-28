
using System.Linq;

namespace Controls;

public abstract class BaseValueConverter : MarkupExtension, IValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider) => this;

    public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

    public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public sealed class FileExtensionToIconConverter : BaseValueConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string file = File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}\\Icons.json");
        List<string> l = JsonSerializer.Deserialize<List<string>>(file);
        string iconpath = "";
        if (Equals(value, Shared.strings.Extension_Folder)) return GetIconPathFromList(l, "31.ico");
        else return GetIconPathFromList(l, "11.ico");
        return null;
    }

    private string GetIconPathFromList(List<string> l, string key)
    {
        string s = "";
        foreach(string icon in l)
        {
            if (icon.Split("\\").Last() == key) 
                return s = icon;
        }
        return s;
    }
}