namespace Services.Plugins
{
    public static class PluginsLoader
    {
        public static readonly List<PluginObject> PluginsList;

        static PluginsLoader()
        {
            PluginsList = new List<PluginObject>();
            foreach (var plugin in Storage.Storage.ExternalCollection.FindAll())
            {
                if (Storage.Storage.ExternalCollection.FindById($"Extension.{plugin.Name.TrimEnd(".dll".ToCharArray())}") != null) PluginsList.Add(new PluginObject(plugin.Name, Assembly.LoadFile(plugin.FullName)));

            }
        }
    }
}

