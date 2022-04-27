namespace Services.Themes;

public static class ThemeLibraryLoader
{
    public static readonly List<ThemeObject> ThemeLibrariesList;

    static ThemeLibraryLoader()
    {
        ThemeLibrariesList = new List<ThemeObject>();
        //foreach(var themelib in Storage.Storage.ExternalCollection.FindAll())
        //{
        //    if (themelib.Name.ToLower().StartsWith("theme")) ThemeLibrariesList.Add(new ThemeObject(themelib.Name, Assembly.LoadFile(themelib.FullName)));
        //}
    }
}