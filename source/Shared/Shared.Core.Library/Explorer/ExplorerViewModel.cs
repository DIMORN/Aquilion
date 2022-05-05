namespace Shared.Core.Library.Explorer;

public class ExplorerViewModel : BindableBase, INavigatable
{
    #region Private Fields
    private FileExplorerViewModel _fileExplorerViewModel;
    #endregion

    #region Public Properties
    /// <summary>
    /// Public link to _fileExplorerViewModel;
    /// </summary>
    public FileExplorerViewModel FileExplorerViewModel => _fileExplorerViewModel;

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

        var obj = Storage.GenericCollection.FindById(Locale.Locale.Storage_Object_Names_Computer);
        Navigation = new Navigation(obj.FullName, obj.Name);

        Load(Navigation.Current.Path);

        Navigation.Navigated += Navigation_Navigated;
    }

    #endregion

    #region Commands
    /// <summary>
    /// Command for invoke GoBack method
    /// </summary>
    public DelegateCommand GoBackCommand { get; set; }

    /// <summary>
    /// Command for invoke GoForward method
    /// </summary>
    public DelegateCommand GoForwardCommand { get; set; }

    /// <summary>
    /// Command for invoke OnOpen method
    /// </summary>
    public DelegateCommand<object> OpenCommand { get; set; }
    #endregion

    #region Commands Methods
    public void OnOpen(object parameter)
    {
        switch (parameter)
        {
            case DirectoryModel dm:
                if (dm.FullName.ToLower() == ((FileExplorerViewModel)Current).Path.ToLower())
                {
                    Load(dm.FullName + "\\");
                }
                else
                {
                    ((FileExplorerViewModel)Current).Name = dm.Name;
                    ((FileExplorerViewModel)Current).Path = dm.FullName;
                    Navigation.Add(((FileExplorerViewModel)Current).Path + "\\", ((FileExplorerViewModel)Current).Name);
                    Load(((FileExplorerViewModel)Current).Path + "\\");
                }
                break;
            case DriveModel driveModel:
                if (driveModel.FullName.ToLower() == ((FileExplorerViewModel)Current).Path.ToLower())
                {
                    Load(driveModel.FullName);
                }
                else
                {
                    ((FileExplorerViewModel)Current).Name = driveModel.Name;
                    ((FileExplorerViewModel)Current).Path = driveModel.FullName;
                    Navigation.Add(((FileExplorerViewModel)Current).Path, ((FileExplorerViewModel)Current).Name);
                    ((FileExplorerViewModel)Current).Load(((FileExplorerViewModel)Current).Path);
                }
                break;
            case string path:
                if (path.ToLower() == ((FileExplorerViewModel)Current).Path.ToLower())
                {
                    Load(path + "\\");
                }
                else
                {
                    Navigation.Add(path + "\\", path.Split("\\").Last());
                    Load(path + "\\");

                }
                break;
               

            case ObservableCollection<FileSystemModel> SelectedModels:
                ((FileExplorerViewModel)Current).OpenCommand.Execute();
                break;
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

    private bool OnCanGoBack()
    {
        return Navigation.CanGoBack;
    }
    private void OnGoBack()
    {
        Navigation.GoBack();
        Load(Navigation.Current.Path);
    }
    private bool OnCanGoForward()
    {
        return Navigation.CanGoForward;
    }
    private void OnGoForward()
    {
        Navigation.GoForward();
        Load(Navigation.Current.Path);
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
                ((FileExplorerViewModel)Current).Load(p);
            }
            else
            {
                _fileExplorerViewModel.Name = Navigation.Current.Name;
                _fileExplorerViewModel.Path = Navigation.Current.Path;
                Current = _fileExplorerViewModel;
                ((FileExplorerViewModel)Current).Load(p);
            }
            CurrentType = ExplorerModelType.FileView;
        }
    }

    private void Init()
    {
        _fileExplorerViewModel = new FileExplorerViewModel(this);
        OpenCommand = new DelegateCommand<object>(OnOpen);
        //Init Menu
        {
            InitializeMenuItems();
            var ch = Menu[2].Children[1] as CheckableMenuItemViewModel;
            ch.IsChecked = true;
        }

        //Init Commands
        {
            GoBackCommand = new DelegateCommand(OnGoBack, OnCanGoBack);
            GoForwardCommand = new DelegateCommand(OnGoForward, OnCanGoForward);
        }
    }

    private void InitializeMenuItems(IList<StorageObject>? list = null, MenuItemViewModel parent = null)
    {

        Menu.Add(new MenuItemViewModel
        {
            Header = "File",
            Children = new List<MenuItemViewModel>
            {
                new MenuItemViewModel
                {
                    Header = "Open",
                    Command = new DelegateCommand<object>(OnOpen),
                    CommandParameter = _fileExplorerViewModel.SelectedModels
                },
                new MenuItemViewModel
                {
                    Header = "Exit",
                    Command = new DelegateCommand<object>(OnClosing)
                }
            }

        });
        Menu.Add(new MenuItemViewModel
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
        Menu.Add(new MenuItemViewModel
        {
            Header = "View",
            Children = new List<MenuItemViewModel>
            {
                
                new CheckableMenuItemViewModel("Icons", Check, true, "View"),
                new CheckableMenuItemViewModel("Details", Check, true, "View"),
                new CheckableMenuItemViewModel("Checkable List Items", Check),
            }

        });
        Menu.Add(new MenuItemViewModel
        {
            Header = "Favorites"

        });
        Menu.Add(new MenuItemViewModel
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
        Menu.Add(new MenuItemViewModel
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

        
    }

    private void Check(object? sender, PropertyChangedEventArgs e)
    {
        if(sender is CheckableMenuItemViewModel chk)
            if(chk.Header == "Checkable List Items")
            {
                if(Current is FileExplorerViewModel fileExplorerViewModel)
                {
                    fileExplorerViewModel.CheckableListItems = chk.IsChecked;
                }
            }
    }


    private void Navigation_Navigated(object? sender, EventArgs e)
    {
        GoBackCommand.RaiseCanExecuteChanged();
        GoForwardCommand.RaiseCanExecuteChanged();
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
