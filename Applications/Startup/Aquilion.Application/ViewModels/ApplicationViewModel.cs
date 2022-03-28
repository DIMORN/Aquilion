using System.Diagnostics;

namespace Aquilion.Startup;

public class ApplicationViewModel : Shared.BaseViewModel
{
    private DispatcherTimer _timer = new DispatcherTimer()
    {
        Interval = TimeSpan.FromSeconds(1)
    };
    public string? DebugInfo { get; set; }

    public ApplicationViewModel()
    {
        _timer.Tick += _timer_Tick;
        _timer.Start();
        ThemeService.Styling(ThemeService.GetThemeLibrary());
    }

    private void _timer_Tick(object? sender, EventArgs e)
    {
        DebugInfo = Startup.App.Current.Resources.Values.ToString();
        foreach(var d in App.Current.Resources.MergedDictionaries)
        {
            foreach(var s in d.Values)
            {
                DebugInfo += "\n" + d.Source.AbsoluteUri;
            }
        }
    }
}
