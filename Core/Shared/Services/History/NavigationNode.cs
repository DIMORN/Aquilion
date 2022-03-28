namespace Shared;

internal class NavigationNode
{
    public NavigationNode PreviousNode { get; set; }
    public NavigationNode NextNode { get; set; }
    public string? Path { get; }
    public string? PathName { get; }
    public NavigationNode(string path, string pathname)
    {
        Path = path;
        PathName = pathname;
    }
}