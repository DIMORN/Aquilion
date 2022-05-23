namespace Services.Plugins
{
    public class PluginObject
    {
        public string? Key { get; set; }
        public Assembly? Assembly { get; set; }
        public PluginObject(string? key, Assembly? assembly)
        {
            Key = key;
            Assembly = assembly;
        }
    }

    public interface IPlugin
    {
        void DoWork();
    }
}