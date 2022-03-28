namespace Shared;

internal interface IHistory : IEnumerable<NavigationNode>
{
    bool CanGoBack { get; }

    bool CanGoForward { get; }

    NavigationNode Current { get; }

    event EventHandler HistoryChanged;

    void GoBack();

    void GoForward();
    void Add(string path, string name);
}