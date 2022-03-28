namespace Shared;
internal class NavigationHistory : IHistory
{
    #region Private Fields

    private NavigationNode _first;

    #endregion

    #region Public Properties

    public bool CanGoBack => Current.PreviousNode != null;
    public bool CanGoForward => Current.NextNode != null;
    public NavigationNode Current { get; private set; }

    #endregion

    #region Events

    public event EventHandler? HistoryChanged;

    #endregion

    #region Constructor
    public NavigationHistory(string path, string pathname)
    {
        _first = new NavigationNode(path, pathname);
        Current = _first;
    }

    #endregion


    #region Public Methods
    public void GoBack()
    {
        var prev = Current.PreviousNode;

        Current = prev!;

        RaiseHistoryChanged();
    }

    public void GoForward()
    {
        var next = Current.NextNode;

        Current = next!;

        RaiseHistoryChanged();
    }

    public void Add(string path, string name)
    {
        var node = new NavigationNode(path, name);

        Current.NextNode = node;
        node.PreviousNode = Current;

        Current = node;

        RaiseHistoryChanged();
    }

    #endregion

    #region Private Methods

    private void RaiseHistoryChanged() => HistoryChanged?.Invoke(this, EventArgs.Empty);

    #endregion

    #region Enumerator

    public IEnumerator<NavigationNode> GetEnumerator()
    { 
        yield return Current;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion
}