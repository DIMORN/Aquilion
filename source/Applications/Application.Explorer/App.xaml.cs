using Controls;
using System.Reflection;

namespace Application.Explorer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private object DataContext { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DataContext = new ExplorerViewModel(new ViewModels.FileExplorerViewModel());
            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = DataContext;
            mainWindow.Show();

            Current.Dispatcher.InvokeAsync(()=> LoadExtensionsAndThemes());
        }

        private void LoadExtensionsAndThemes()
        {
            //Themes
            {
                Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri($"pack://application:,,,/Controls;component/Styles/Generic.xaml")
                });

                if (ThemeLibraryLoader.ThemeLibrariesList.Count != 0)
                {


                }
                else
                {
                    Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                    {
                        Source = new Uri($"pack://application:,,,/{ThemeLibraryLoader.DefaultThemeAssembly.FullName};component/Generic.xaml")
                    });
                }

            }

            //Plugins
            {
                if (PluginsLoader.PluginsList.Count != 0)
                {
                    foreach (var plugin in PluginsLoader.PluginsList)
                    {
                        var asm = plugin.Assembly;
                        if (plugin.Key.Contains("UI"))
                        {
                            //UserControl uc;
                            //var pluginname = plugin.Key.TrimEnd($".{plugin.Key.Split('.').Last()}".ToCharArray());
                            //var t = asm.ExportedTypes.ToList().Find(x => x.Name == pluginname.Split('.').Last());
                            //var obj = Activator.CreateInstance(t);
                            //uc = obj as UserControl;

                            //if (MainWindow.Content is DockPanel dockPanel)
                            //{
                            //    var uilist = new List<UIElement>();
                            //    foreach (UIElement UIElement in dockPanel.Children)
                            //    {
                            //        if (UIElement.GetType().Name == t.Name)
                            //        {
                            //            dockPanel.Children.Remove(UIElement);
                            //            dockPanel.Children.Add(uc);

                            //        }
                            //        else
                            //        {
                            //            continue;
                            //            DockPanel.SetDock(uc, Dock.Left);
                            //            dockPanel.Children.Add(uc);
                            //        }
                            //        break;
                            //    }
                            //}
                        }
                        else if (plugin.Key.Contains("Exporer.Core"))
                        {
                            foreach(var type in asm.ExportedTypes)
                            {
                                var c = Activator.CreateInstance(type, DataContext);
                                if (c is IPlugin)
                                    ((IExtensible)DataContext).Plugins.Add(c as IPlugin);
                            }
                        }
                    }
                }
            }
        }
    }
}
