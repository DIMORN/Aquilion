using System.Reflection;

namespace Service.Plugins
{
    public class Plugin
    {
        public string Key { get; set; }
        public Assembly Assembly { get; set; }

        public Plugin(
            string key,
            Assembly assembly)
        {
            Key = key;
            Assembly = assembly;
        }
    }
}