namespace Services.Themes;

public class ThemeObject
{
    public string? Key { get; set; }
    public Assembly? Assembly { get; set; }
    public ThemeObject(string? key, Assembly? assembly)
    {
        Key = key;
        Assembly = assembly;
    }
}
