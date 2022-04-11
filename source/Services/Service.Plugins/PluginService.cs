using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Service.Plugins
{
    public static class PluginService
    {
        public static string PluginsDirectory;
        public static List<Plugin> Plugins = new List<Plugin>();

        static PluginService()
        {
            PluginsDirectory =
                $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Aquilion\\Plugins\\";

            LoadPlugins();
        }

        private static void LoadPlugins()
        {
            DirectoryInfo di = new DirectoryInfo(PluginsDirectory);
            foreach (var enumerateDirectory in di.EnumerateDirectories())
            {
                Plugins.Add(new Plugin(
                    enumerateDirectory.Name,
                    Assembly.LoadFile($"{enumerateDirectory.FullName}\\bin\\Debug\\netcoreapp3.1\\{enumerateDirectory.Name}.dll")));
            }
        }
    }
}
