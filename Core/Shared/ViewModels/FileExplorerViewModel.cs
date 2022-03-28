namespace Shared;

public class FileExplorerViewModel : ShellViewViewModel, IFileExplorer, INavigable
{
    #region Private Fields
    /// <summary>
    /// Field of navigation history
    /// </summary>
    private IHistory _history;
    /// <summary>
    /// Field of Background Worker
    /// </summary>
    private BackgroundWorker _worker;
    /// <summary>
    /// Filed of changed property
    /// </summary>
    private object _changedProperty;
    #endregion

    #region Public Properties
    /// <summary>
    /// Name of current place
    /// </summary>
    public string? CurrentName { get; set; }
    /// <summary>
    /// Path to current place
    /// </summary>
    public string? CurrentPath { get; set; }
    
    /// <summary>
    /// Collection of File System Models
    /// </summary>
    public ObservableCollection<IFileSystemModel>? FileSystemModelsCollection { get; set; } =
        new ObservableCollection<IFileSystemModel>();
    
    /// <summary>
    /// Menu
    /// </summary>
    public ObservableCollection<MenuItemViewModelBase> Menu { get; set; } =
        new ObservableCollection<MenuItemViewModelBase>();

    public ObservableCollection<FileSystemModel>? SelectedModels { get; set; } =
        new ObservableCollection<FileSystemModel>();
    #endregion

    #region Constructor
    /// <summary>
    /// Default constructor
    /// </summary>
    public FileExplorerViewModel()
    {
        InitializeMenu();
        
        _worker = new BackgroundWorker();
        _history = new NavigationHistory(@"myComputer:\", strings.SystemFolder_Computer);

        OpenFileSystemModelCommand = new DelegateCommand(OnOpenFileSystemModel);
        GoBackCommand = new DelegateCommand(OnGoBack, OnCanGoBack);
        GoForwardCommand = new DelegateCommand(OnGoForward, OnCanGoForward);

        CurrentName = _history.Current.PathName;
        CurrentPath = _history.Current.Path;

        Load(CurrentPath);

        _history.HistoryChanged += _history_HistoryChanged;

        this.PropertyChanged += FileExplorerViewModel_PropertyChanged;
    }

    
    #endregion

    #region Commands

    public DelegateCommand? OpenFileSystemModelCommand { get; }
    public DelegateCommand? GoBackCommand { get; }
    public DelegateCommand? GoForwardCommand { get; }
    public DelegateCommand? GoParentCommand { get; }
    
    #endregion

    #region Commands Methods
    private void OnOpenFileSystemModel(object obj)
    {
        if (obj is DirectoryModel directoryModel)
        {
            CurrentPath = directoryModel.FullName;
            CurrentName = directoryModel.Name;

            _history.Add(CurrentPath, CurrentName);

            Load(CurrentPath);
        }
        if (obj is DriveModel driveModel)
        {
            CurrentPath = driveModel.FullName;
            CurrentName = driveModel.Name; 
            _history.Add(CurrentPath, CurrentName);

            Load(CurrentPath);
        }
    }
    private bool OnCanGoForward(object obj) => _history.CanGoForward;

    private void OnGoForward(object obj)
    {
        _history.GoForward();

        var current = _history.Current;

        CurrentPath = current.Path;
        CurrentName = current.PathName;

        Load(CurrentPath);
    }
    private bool OnCanGoBack(object obj) => _history.CanGoBack;

    private void OnGoBack (object obj)
    {
        _history.GoBack();

        var current = _history.Current;

        CurrentPath = current.Path;
        CurrentName = current.PathName;

        Load(CurrentPath);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Defines load method by path
    /// </summary>
    /// <param name="path"> Full path </param>
    private void Load(string path)
    {
        FileSystemModelsCollection.Clear();
        SelectedModels.Clear();
        MaximumItemsCount = 0;
        if (path == @"myComputer:\")
        {
            LoadMyComputer();
            MaximumItemsCount = FileSystemModelsCollection.Count;
            return;
        }
        else
        {
            LoadDirectoriesAndFiles(path);
            MaximumItemsCount = FileSystemModelsCollection.Count;
        }
        
    }

    /// <summary>
    /// Load my computer place
    /// </summary>
    private void LoadMyComputer()
    {
        foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
        {
            FileSystemModelsCollection.Add(CreateFileSystemModel(driveInfo));
        }
    }

    /// <summary>
    /// Load directory by path
    /// </summary>
    /// <param name="path"> Full path to directory </param>
    private void LoadDirectoriesAndFiles(string path)
    {
        DirectoryInfo di = new DirectoryInfo(path);
        foreach (DirectoryInfo enumerateDirectory in di.EnumerateDirectories())         
        {
            if(!(enumerateDirectory.Attributes.HasFlag(FileAttributes.System)
                || enumerateDirectory.Attributes.HasFlag(FileAttributes.Hidden)
                || enumerateDirectory.Attributes.HasFlag(FileAttributes.Encrypted)))
                FileSystemModelsCollection.Add(CreateFileSystemModel(enumerateDirectory));
            
        }
        foreach (FileInfo enumerateFile in di.EnumerateFiles())
        {
            if (!(enumerateFile.Attributes.HasFlag(FileAttributes.System)
                || enumerateFile.Attributes.HasFlag(FileAttributes.Hidden)
                || enumerateFile.Attributes.HasFlag(FileAttributes.Encrypted)))
                FileSystemModelsCollection.Add(CreateFileSystemModel(enumerateFile));
            
        }
    }

    /// <summary>
    /// Initializes menu
    /// </summary>
    private void InitializeMenu()
    {
        Menu.Add(NewMenuItemViewModel(
            "File",
            null,
            null,
            new List<MenuItemViewModelBase>
            {
                NewMenuItemViewModel("Open"),
                NewMenuItemViewModel("",null,null,null,null,null,true),
                NewMenuItemViewModel("Rename"),
                NewMenuItemViewModel("",null,null,null,null,null,true),
                NewMenuItemViewModel("New Folder"),
                NewMenuItemViewModel("",null,null,null,null,null,true),
                NewMenuItemViewModel("Close"),
            }));
        Menu.Add(NewMenuItemViewModel(
            "Edit",
            null,
            null,
            new List<MenuItemViewModelBase>
            {
                NewMenuItemViewModel("Copy"),
                NewMenuItemViewModel("Cut"),
                NewMenuItemViewModel("Paste")
            }));
        Menu.Add(NewMenuItemViewModel(
            "View",
            null,
            null,
            new List<MenuItemViewModelBase>
            {
                NewMenuItemViewModel(
                    "Preview",
                    null,
                    null,
                    null,
                    true),
                NewMenuItemViewModel("",null,null,null,null,null,true),
                NewMenuItemViewModel("Icons", null, null, null, true, "View"),
                NewMenuItemViewModel("Detail", null, null, null, true, "View"),
                NewMenuItemViewModel("Extended Tiles", null, null, null, true, "View")
            }));
        Menu.Add(NewMenuItemViewModel(
            "Favorites",
            null,
            null,
            new List<MenuItemViewModelBase>
            {
                NewMenuItemViewModel("my Documets"),
                NewMenuItemViewModel("User"),
                NewMenuItemViewModel("Computer")
            }));
        Menu.Add(NewMenuItemViewModel(
            "Tools",
            null,
            null,
            new List<MenuItemViewModelBase>
            {
                NewMenuItemViewModel("Folder Options")
            }));
        Menu.Add(NewMenuItemViewModel(
            "Help",
            null,
            null,
            new List<MenuItemViewModelBase>
            {
                NewMenuItemViewModel("View All Activities"),
                NewMenuItemViewModel("",null,null,null,null,null,true),
                NewMenuItemViewModel("About Explorer")
            }));

    }

    /// <summary>
    /// Occurs when history changed
    /// </summary>
    /// <param name="sender"> File Explorer View Model </param>
    /// <param name="e"> Empty </param>
    private void _history_HistoryChanged(object? sender, EventArgs e)
    {
        GoBackCommand?.RaiseCanExecuteChanged();
        GoForwardCommand?.RaiseCanExecuteChanged();
    }

    /// <summary>
    /// Method for create menu items
    /// </summary>
    /// <param name="header"> Set header </param>
    /// <param name="command"> Set command</param>
    /// <param name="parameter"> Set command parameter</param>
    /// <param name="children"> Set children menus collection </param>
    /// <param name="checkable"> Set to can an menu item be checked </param>
    /// <param name="group"> If group = "", menu item be as check box else as radio button </param>
    /// <param name="separator"> Does it separator </param>
    /// <returns>Menu item</returns>
    private MenuItemViewModelBase NewMenuItemViewModel(
        string header,
        DelegateCommand? command = null,
        object? parameter = null,
        IList<MenuItemViewModelBase>? children = null,
        bool? checkable = null,
        string? group = null,
        bool? separator = null)
    {
        MenuItemViewModelBase? menuItem;

        if (checkable == true)
        {
            if (!string.IsNullOrEmpty(group))
            {
                menuItem = new GroupedCheckableMenuItemViewModel(
                header, group);
                menuItem.PropertyChanged += MenuItem_PropertyChanged;
                return menuItem;
            }
            else
            {
                menuItem = new CheckableMenuItemViewModel(
                header);
                menuItem.PropertyChanged += MenuItem_PropertyChanged;
                return menuItem;
            }
        }
        else
        {
            menuItem = new DefaultMenuItemViewModel(
                header,
                command,
                parameter,
                children,
                separator);
            return menuItem;
        }
    }

    /// <summary>
    /// Occurs when menu item's property changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MenuItem_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        var o = sender as CheckableMenuItemViewModel;
        if (e.PropertyName == nameof(o.IsChecked))
        {
            var c = typeof(FileExplorerViewModel).GetProperty(o.Header);
            if(c!=null) c.SetValue(this, o.IsChecked);
        }
    }

    private void UncheckMenuItemByHeader(IList<MenuItemViewModelBase> menu, string header)
    {
        foreach(var menuitem in menu)
        {
            if(menuitem is CheckableMenuItemViewModel chk)
            {
                if (chk.Header == header)
                {
                    chk.IsChecked = (bool?)this.GetType().GetProperty(header).GetValue(this);
                }
            }
        }
        foreach(MenuItemViewModelBase menuItem2 in menu)
        {
            if(menuItem2.Children != null)
            {
                UncheckMenuItemByHeader(menuItem2.Children, header);
            }
        }
        
    }

    private void FileExplorerViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        UncheckMenuItemByHeader(Menu, e.PropertyName);
    }

    private FileSystemModel? CreateFileSystemModel(object o)
    {
        if(o is FileInfo fileInfo)
        {
            var f = new FileModel(fileInfo);
            f.PropertyChanged += FileSystemModel_PropertyChanged;
            return f;
        }
        else if (o is DirectoryInfo directoryInfo)
        {
            var d = new DirectoryModel(directoryInfo);
            d.PropertyChanged += FileSystemModel_PropertyChanged;
            return d;
        }
        else if (o is DriveInfo driveInfo)
        {
            var h = new DriveModel(driveInfo);
            h.PropertyChanged += FileSystemModel_PropertyChanged;
            return h;
        }

        return null;
    }

    private void FileSystemModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if(sender is ISelectableModel selectable)
        {
            SelectedModels.Clear();
            if ((bool)selectable.IsSelected)
            {
                SelectedModels.Add((FileSystemModel)selectable);
            }
            else if (!(bool)selectable.IsSelected)
            {
                SelectedModels.Remove((FileSystemModel)selectable);
            }
        }
    }
    #endregion
}
