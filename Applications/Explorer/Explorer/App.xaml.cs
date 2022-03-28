using System.Resources;

using System.Windows.Resources;

namespace Explorer;

public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

        ThemeService.Styling(ThemeService.GetThemeLibrary());

        var win = new AquilionWindow();
        win.DataContext = new FileExplorerViewModel();
        win.ToolBar = new ExplorerToolBar();
        win.Content = new MainView();
        win.Show();

        
    }

   
}