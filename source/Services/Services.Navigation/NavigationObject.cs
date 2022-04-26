namespace Services.Navigation;

public class NavigationObject : BindableBase
{
    public NavigationObject? PREVIOUS { get; set; }
    public NavigationObject? NEXT { get; set; }
    public string? Name { get; set; }
    public string? Path { get; set; }
    public NavigationObject(string path, string name)
    {
        Name = name;
        Path = path;
    }
}
