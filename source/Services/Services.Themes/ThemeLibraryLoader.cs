namespace Services.Themes
{
    public static class ThemeLibraryLoader
    {
        public static readonly List<ThemeObject> ThemeLibrariesList;
        public static readonly Assembly DefaultThemeAssembly;
        static ThemeLibraryLoader()
        {
            DefaultThemeAssembly = Assembly.LoadFile($"{Storage.Storage.ExternalServicesDirectory}/Themes/Plex/bin/debug/netcoreapp3.1/Plex.dll");
            ThemeLibrariesList = new List<ThemeObject>();
            //foreach(var themelib in Storage.Storage.ExternalCollection.FindAll())
            //{
            //    if (themelib.Name.ToLower().StartsWith("theme")) ThemeLibrariesList.Add(new ThemeObject(themelib.Name, Assembly.LoadFile(themelib.FullName)));
            //}
        }
    }

}