using Services.Plugins;

namespace Shared.Core.Library.Explorer
{
    public class ExplorerViewModel : BaseViewModel, INavigatable, IExtensible
    {
        #region Private Fields
        private IFileExplorer _fileExplorerViewModel;
        private Lazy<string> _firstCheckedMenuItemHeader;
        #endregion
    
        #region Public Properties
        /// <summary>
        /// Public link to _fileExplorerViewModel;
        /// </summary>
        public IFileExplorer FileExplorerViewModel { get; set; } 

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

        public List<IPlugin> Plugins { get; set; } = new();
        #endregion
    
        #region Constructor 

        /// <summary>
        /// Default constructor
        /// </summary>
        public ExplorerViewModel(IFileExplorer fileExplorerViewModel)
        {
            Init(fileExplorerViewModel);

            var obj = Storage.GenericCollection.FindById(Locale.Locale.Storage_Object_Names_Computer);
            Navigation = new Navigation(obj.FullName, obj.Name);

            var CURR = Navigation.Current;
            ((IExplorer)FileExplorerViewModel).CurrentModel = FileSystemModel.Create(obj);
            ((IExplorer)FileExplorerViewModel).Path = CURR.Path;
            ((IExplorer)FileExplorerViewModel).Name = CURR.Name;
            
            Load(CURR.Path);

            Navigation.Navigated += Navigation_Navigated;
        }

        public ExplorerViewModel()
        {

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
                    if (dm.FullName.ToLower() == Navigation.Current.Path.ToLower())
                    {
                        Load(dm.FullName + "\\");
                    }
                    else
                    {
                        ((IExplorer)Current).CurrentModel = dm;
                        ((IExplorer)Current).Name = ((IExplorer)Current).CurrentModel.Name;
                        ((IExplorer)Current).Path = ((IExplorer)Current).CurrentModel.FullName;
                        Navigation.Add(((IExplorer)Current).Path + "\\", ((IExplorer)Current).Name);
                        Load(((IExplorer)Current).Path + "\\");
                    }
                    break;
                case DriveModel driveModel:
                    if (driveModel.FullName.ToLower() == Navigation.Current.Path.ToLower())
                    {
                        Load(driveModel.FullName);
                    }
                    else
                    {
                        ((IExplorer)Current).CurrentModel = driveModel;
                        ((IExplorer)Current).Name = ((IExplorer)Current).CurrentModel.Name;
                        ((IExplorer)Current).Path = ((IExplorer)Current).CurrentModel.FullName;
                        Navigation.Add(((IExplorer)Current).Path + "\\", ((IExplorer)Current).Name);
                        Load(((IExplorer)Current).Path + "\\");
                    }
                    break;
                case string path:
                    if (path.ToLower() == Navigation.Current.Path.ToLower())
                    {
                        Load(path + "\\");
                    }
                    else
                    {
                        Navigation.Add(path + "\\", path.Split("\\").Last());
                        Load(path + "\\");

                    }
                    break;
                case StorageObjectModel storageObjectModel:
                    if (storageObjectModel.FullName.ToLower() == Navigation.Current.Path.ToLower())
                    {
                        Load(storageObjectModel.FullName);
                    }
                    else
                    {
                        ((IExplorer)Current).CurrentModel = storageObjectModel;
                        ((IExplorer)Current).Name = ((IExplorer)Current).CurrentModel.Name;
                        ((IExplorer)Current).Path = ((IExplorer)Current).CurrentModel.FullName;
                        Navigation.Add(((IExplorer)Current).Path + "\\", ((IExplorer)Current).Name);
                        Load(((IExplorer)Current).Path + "\\");
                    }
                    break;
                case StorageObject storageObject:
                    var obj = FileSystemModel.Create(storageObject);
                    OpenCommand.Execute(obj);
                    break;

                case ObservableCollection<FileSystemModel>:
                    ((IFileExplorer)Current).OpenCommand.Execute();
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
        public void OnGoBack()
        {
            Navigation.GoBack();
            var CURR = Navigation.Current;
            ((IExplorer)FileExplorerViewModel).Path = CURR.Path;
            ((IExplorer)FileExplorerViewModel).Name = CURR.Name;
            Load(((IExplorer)FileExplorerViewModel).Path);
        }
        private bool OnCanGoForward()
        {
            return Navigation.CanGoForward;
        }
        private void OnGoForward()
        {
            Navigation.GoForward();
            var CURR = Navigation.Current;
            ((IExplorer)FileExplorerViewModel).Path = CURR.Path;
            ((IExplorer)FileExplorerViewModel).Name = CURR.Name;
            Load(((IExplorer)FileExplorerViewModel).Path);
        }
        #endregion
    
        #region Private Methods
        public virtual void Load(string p)
        {
            if(!p.ToLower().StartsWith("https:\\")
                || !p.ToLower().StartsWith("https:\\"))
            {
                
                if(Navigation.Current.PREVIOUS == null)
                {
                    Current = FileExplorerViewModel;
                    ((IFileExplorer)FileExplorerViewModel).Load(p);
                }
                else
                {
                    Current = FileExplorerViewModel;
                    ((IFileExplorer)FileExplorerViewModel).Load(p);
                }
                CurrentType = ExplorerModelType.FileView;
            }

            foreach (var plugin in Plugins) 
                plugin.DoWork();
        }

        private void Init(IFileExplorer fileExplorerViewModel)
        {
            _fileExplorerViewModel = fileExplorerViewModel;
            _fileExplorerViewModel.ExplorerViewModel = this;
            FileExplorerViewModel = _fileExplorerViewModel;
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
            if (sender is CheckableMenuItemViewModel chk)
            {
                if (chk.Header == "Checkable List Items")
                {
                    if (Current is IFileExplorer fileExplorerViewModel)
                    {
                        FileExplorerViewModel.IsCheckableItems = (bool)chk.IsChecked;
                    }
                }
            }
        }


        private void Navigation_Navigated(object? sender, EventArgs e)
        {
            GoBackCommand.RaiseCanExecuteChanged();
            GoForwardCommand.RaiseCanExecuteChanged();
        }

        public void OnGoBack(object obj)
        {
            GoBackCommand.Execute();
        }
        #endregion
    }

    public interface IExtensible
    {
        List<IPlugin> Plugins { get; set; }
    }

    public interface IExplorer
    {
        IFileSystemModel CurrentModel { get; set; }

        /// <summary>
        /// Path of current place
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// Name of current place
        /// </summary>
        string Name { get; set; }
    }
}

