namespace ExplorerApp;
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        if (ThemeLibraryLoader.ThemeLibrariesList.Count == 0)
        {
            Current.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/ExplorerApp;component/Generic/Generic.xaml")
            });
        }
        else
        {
            var a = Assembly.Load(ThemeLibraryLoader.ThemeLibrariesList.First().Assembly.FullName);
            Current.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri($"pack://application:,,,/{a.FullName};component/Generic/Generic.xaml")
            });
        }
    }
}
