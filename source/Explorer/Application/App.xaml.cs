using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Controls;
using Explorer.Common;
using Explorer.Controls;
using Explorer.Controls.ToolBar;

namespace ExplorerApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Assembly a = Assembly.LoadFile(@"C:\Users\pc\AppData\Local\Aquilion\Resources\Themes\Plex\bin\Debug\netcoreapp3.1\Plex.dll");
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri($"pack://application:,,,/{a.FullName};component/Generic.xaml")
            });

            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                //while (Process.GetProcessesByName("explorer").Length > 0)
                //{
                //    Process.GetProcessesByName("explorer").ToList().ForEach(delegate(Process p)
                //    {
                //        p.Kill();
                //    });
                //}


                var win = new NavigationWindow();
                win.ToolBar = new NavigationWindowToolBar();
                win.Content = new ShellView();
                win.DataContext = new ExplorerViewModel();
                win.Show();
            });
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            Process.Start(new ProcessStartInfo("explorer.exe")
            {
                UseShellExecute = true
            });
        }
    }
}
