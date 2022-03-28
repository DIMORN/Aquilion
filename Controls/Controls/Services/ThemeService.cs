namespace Controls;

public static class ThemeService
{
    public static Assembly? GetThemeLibrary()
    {
        Assembly? a = null;
        foreach (var fi in new DirectoryInfo(Shared.Storage.AppLocalDirectory).EnumerateFiles())
        {
            if (fi.Name.ToLower().Contains("plex.dll"))

            {
                a = Assembly.LoadFile(fi.FullName);
            }
        }
        return a;
    }
     
    public static void Styling(Assembly a)
    {
        string s = $"pack://application:,,,/{a.GetName()};component/Generic.xaml";


        var grd = new ResourceDictionary
        {
            Source = new Uri(s)
        };

        var icons = GetImageResources(Shared.Storage.IconsDirectory);

        string t = "";

        t = JsonSerializer.Serialize(icons, new JsonSerializerOptions
        {
            WriteIndented = true
        });
     File.WriteAllText($"{Shared.Storage.IconsDirectory}\\Icons.json", t);

        Application.Current.Resources.MergedDictionaries.Add(grd);


    }

    /// <summary>
    /// Get icon resources by path
    /// </summary>
    /// <param name="rd"> Resource Dictionary in which be writed resources</param>
    /// <param name="v"> Full path to directory in which be found resorces </param>
    /// <returns></returns>
    private static List<string> GetImageResources(string v)
    {
        List<string> imageBrushes = new List<string>();

        if (v != "")
        {


            foreach (var filename in new DirectoryInfo(v).EnumerateFiles("*.ico"))
            {
                imageBrushes.Add(filename.FullName);
            }
        }

        return imageBrushes;
    }
}
