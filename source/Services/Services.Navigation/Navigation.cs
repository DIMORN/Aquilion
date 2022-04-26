using System.Collections;

namespace Services.Navigation;

public class Navigation : INavigation
{
    #region Private Fields
    /// <summary>
    /// First element in navigation history
    /// </summary>
    private NavigationObject _first;
    #endregion

    #region Public Properties
    /// <summary>
    /// Can return back in navigation history
    /// </summary>
    public bool CanGoBack => Current.PREVIOUS != null;
    /// <summary>
    /// Can go forward in navigation history
    /// </summary>
    public bool CanGoForward => Current.NEXT != null;
    /// <summary>
    /// Current element in navigation history
    /// </summary>
    public NavigationObject? Current { get; set; }
    #endregion

    #region Constructor
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="path">Full path of element</param>
    /// <param name="name">Name of element</param>
    public Navigation(string path, string name)
    {
        _first = new NavigationObject(path, name);
        Current = _first;
    }
    #endregion

    #region Events
    /// <summary>
    /// Event which raises when history changed
    /// </summary>
    public event EventHandler? Navigated;
    #endregion

    #region Private Methods
    public void RaiseNavigationHistoryChanged() => Navigated?.Invoke(this, EventArgs.Empty);
    #endregion

    #region Public Methods
    /// <summary>
    /// Method for add element in navigation history
    /// </summary>
    /// <param name="path">Full path of element</param>
    /// <param name="name">Name of element</param>
    public void Add(string path, string name)
    {
        var node = new NavigationObject(path, name);

        Current.NEXT = node;
        node.PREVIOUS = Current;

        Current = node;

    }

    /// <summary>
    /// Method for return back
    /// </summary>
    public void GoBack()
    {
        var prev = Current.PREVIOUS;

        Current = prev!;

        RaiseNavigationHistoryChanged();
    }

    /// <summary>
    /// Method for go forward
    /// </summary>
    public void GoForward()
    {
        var next = Current.NEXT;

        Current = next!;

        RaiseNavigationHistoryChanged();
    }
    #endregion

    #region Enumerator
    public IEnumerator<NavigationObject> GetEnumerator()
    {
        yield return Current;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    #endregion
}