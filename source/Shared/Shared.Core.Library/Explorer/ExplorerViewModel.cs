namespace Shared.Core.Library.Explorer;

public class ExplorerViewModel : BindableBase, INavigatable
{
    #region Public Properties
    /// <summary>
    /// Instance of navigation history
    /// </summary>
    public INavigation Navigation { get; }

    /// <summary>
    /// Instance of current object
    /// </summary>
    public object Current { get; set; }

    public ExplorerModelType CurrentType { get; set; }
    #endregion

    #region Constructor
    /// <summary>
    /// Default constructor
    /// </summary>
    public ExplorerViewModel()
    {
        var obj = Storage.GenericCollection.FindById(Locale.Locale.Storage_Object_Names_Computer);
        Navigation = new Navigation(obj.FullName, obj.Name);

        Load(Navigation.Current.Path);
    }
    #endregion

    #region Commands
    /// <summary>
    /// Command for invoke GoBack method
    /// </summary>
    public DelegateCommand GoBackCommand { get; }

    /// <summary>
    /// Command for invoke GoForward method
    /// </summary>
    public DelegateCommand GoForwardCommand { get; }
    #endregion

    #region Private Methods
    private void Load(string p)
    {
        if(!p.ToLower().StartsWith("https:\\")
            || !p.ToLower().StartsWith("https:\\"))
        {
            
            if(Navigation.Current.PREVIOUS == null)
            {
                Current = new FileExplorerViewModel()
                {
                    Name = Navigation.Current.Name,
                    Path = Navigation.Current.Path
                };
                Navigation.Current = (NavigationObject)Current;
                ((FileExplorerViewModel)Current).Load(p);
            }
            CurrentType = ExplorerModelType.FileView;
        }
    }
    #endregion
}

public enum ExplorerModelType
{
    FileView,
    InternetView,
    ControlPaneView
}
