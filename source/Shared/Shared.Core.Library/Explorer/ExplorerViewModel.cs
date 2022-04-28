namespace Shared.Core.Library.Explorer;

public class ExplorerViewModel : BindableBase, INavigatable
{
    #region Private Fields
    private FileExplorerViewModel _fileExplorerViewModel;
    #endregion

    #region Public Properties
    /// <summary>
    /// Instance of navigation history
    /// </summary>
    public INavigation Navigation { get; }

    /// <summary>
    /// Instance of current object
    /// </summary>
    public object Current { get; set; }
    /// <summary>
    /// Type of current present
    /// </summary>
    public ExplorerModelType CurrentType { get; set; }
    /// <summary>
    /// Main menu collection
    /// </summary>
    public ObservableCollection<MenuItemViewModel> Menu { get; set; } =
        new();
    #endregion

    #region Constructor
    /// <summary>
    /// Default constructor
    /// </summary>
    public ExplorerViewModel()
    {
        Init();
        _fileExplorerViewModel = new FileExplorerViewModel();
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

    #region Commands Methods
    private void OnOpen(object parameter)
    {
        if(parameter is DirectoryModel dm)
        {
            ((FileExplorerViewModel)Current).Name = dm.Name;
            ((FileExplorerViewModel)Current).Path = dm.FullName;
            Navigation.Add(dm.FullName, dm.Name);
            ((FileExplorerViewModel)Current).Load(dm.FullName);
        }
        else if (parameter is DriveModel driveModel)
        {
            ((FileExplorerViewModel)Current).Name = driveModel.Name;
            ((FileExplorerViewModel)Current).Path = driveModel.FullName;
            Navigation.Add(driveModel.FullName, driveModel.Name);
            ((FileExplorerViewModel)Current).Load(driveModel.FullName);
        }
    }
    private void OnClosing(object parameter)
    {
        System.Diagnostics.Process.GetCurrentProcess().Kill();
    }
    private void OnCopy(object parameter)
    {
        
    }
    private void OnPaste(object parameter)
    {
        
    }
    #endregion

    #region Private Methods
    private void Load(string p)
    {
        if(!p.ToLower().StartsWith("https:\\")
            || !p.ToLower().StartsWith("https:\\"))
        {
            
            if(Navigation.Current.PREVIOUS == null)
            {
                _fileExplorerViewModel.Name = Navigation.Current.Name;
                _fileExplorerViewModel.Path = Navigation.Current.Path;
                Current = _fileExplorerViewModel;
                Navigation.Current = (NavigationObject)Current;
                ((FileExplorerViewModel)Current).Load(p);
            }
            CurrentType = ExplorerModelType.FileView;
        }
    }

    private void Init()
    {
        //Init Menu
        {
            InitializeMenuItems();
            var ch = Menu[2].Children[1] as CheckableMenuItemViewModel;
            ch.IsChecked = true;
        }
    }

    private void InitializeMenuItems(IList<StorageObject>? list = null, MenuItemViewModel parent = null)
    {

        
        Storage.DataBase.GetCollection<MenuItemViewModel>("Menu").Upsert("ExplorerMenu.File", new MenuItemViewModel
        {
            Header = "File",
            Children = new List<MenuItemViewModel>
            {
                new MenuItemViewModel
                {
                    Header = "Open"
                },
                new MenuItemViewModel
                {
                    Header = "Exit"
                }
            }

        });
        Storage.DataBase.GetCollection<MenuItemViewModel>("Menu").Upsert("ExplorerMenu.Edit", new MenuItemViewModel
        {
            Header = "Edit",
            Children = new List<MenuItemViewModel>
            {
                new MenuItemViewModel
                {
                    Header = "Copy"
                },
                new MenuItemViewModel
                {
                    Header = "Paste"
                }
            }

        });
        Storage.DataBase.GetCollection<MenuItemViewModel>("Menu").Upsert("ExplorerMenu.View", new MenuItemViewModel
        {
            Header = "View",
            Children = new List<MenuItemViewModel>
            {
                new CheckableMenuItemViewModel("Preview", Check),
                new CheckableMenuItemViewModel("Icons", Check, true, "View"),
                new CheckableMenuItemViewModel("Details", Check, true, "View"),
            }

        });
        Storage.DataBase.GetCollection<MenuItemViewModel>("Menu").Upsert("ExplorerMenu.Favorites", new MenuItemViewModel
        {
            Header = "Favorites"

        });
        Storage.DataBase.GetCollection<MenuItemViewModel>("Menu").Upsert("ExplorerMenu.Tools", new MenuItemViewModel
        {
            Header = "Tools",
            Children = new List<MenuItemViewModel>
            {
                new MenuItemViewModel
                {
                    Header = "Folder Options"
                }
            }

        });
        Storage.DataBase.GetCollection<MenuItemViewModel>("Menu").Upsert("ExplorerMenu.Help", new MenuItemViewModel
        {
            Header = "Help",
            Children = new List<MenuItemViewModel>
            {
                new MenuItemViewModel
                {
                    Header = "About Explorer"
                }
            }

        });

        var c = Storage.DataBase.GetCollection<MenuItemViewModel>("Menu").FindOne(x => x.Header == "File");
        c.Children[0].Command = new DelegateCommand<object>(OnOpen);
        c.Children[1].Command = new DelegateCommand<object>(OnOpen);
        Menu.Add(c);
        var d = Storage.DataBase.GetCollection<MenuItemViewModel>("Menu").FindOne(x => x.Header == "Edit");
        c.Children[0].Command = new DelegateCommand<object>(OnCopy);
        c.Children[1].Command = new DelegateCommand<object>(OnPaste);
        Menu.Add(d);
        Menu.Add(Storage.DataBase.GetCollection<MenuItemViewModel>("Menu").FindOne(x => x.Header == "View"));
        Menu.Add(Storage.DataBase.GetCollection<MenuItemViewModel>("Menu").FindOne(x => x.Header == "Favorites"));
        Menu.Add(Storage.DataBase.GetCollection<MenuItemViewModel>("Menu").FindOne(x => x.Header == "Tools"));
        Menu.Add(Storage.DataBase.GetCollection<MenuItemViewModel>("Menu").FindOne(x => x.Header == "Help"));

        
    }

    private void Check(object? sender, PropertyChangedEventArgs e)
    {

    }
    #endregion
}

public enum ExplorerModelType
{
    /// <summary>
    /// File view present type
    /// </summary>
    FileView,

    /// <summary>
    /// Web view present type
    /// </summary>
    InternetView,

    /// <summary>
    /// Control pane present type
    /// </summary>
    ControlPaneView
}
