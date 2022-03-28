namespace Aquilion.Startup;

public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var win = new AquilionWindow();
        win.Title = "Aquilion Application1";
        var t = new TextBlock();
        var b = new Binding
        {
            Path = new PropertyPath("DebugInfo")
        };
        t.SetBinding(TextBlock.TextProperty, b);
        win.DataContext = new ApplicationViewModel();
        win.Show();
       
    }
}
