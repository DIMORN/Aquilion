using Services.HelpSupport;
using Services.Storage;
using Shared.Core.Library.Selectable;

namespace Application.Explorer.ViewModels
{
    public class FileExplorerViewModel : BaseViewModel, IFileExplorer, IFilterableFileExplorer, IExplorer
    {
        #region Public Fields
        public ObservableCollection<FileSystemModel>? _fileSystemCollection { get; set; } =
            new();
        public ObservableCollection<MenuItemViewModel> _tasks { get; set; } =
            new();
        #endregion

        #region Private Fields
        private string _path;
        private string _name;
        private bool _isCheckableItems;
        private IFileSystemModel _currentModel;
        #endregion

        #region Public Properties
        /// <summary>
        /// Link to main explorer view model
        /// </summary>
        public ExplorerViewModel ExplorerViewModel { get; set; }
        public IFileSystemModel CurrentModel
        {
            get => _currentModel;
            set
            {
                _currentModel = value;
                OnPropertyChanged();
            }
        }

        public string Path
        {
            get => _path;
            set
            {
                _path = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public bool IsCheckableItems
        {
            get => _isCheckableItems;
            set
            {
                _isCheckableItems = value;
                OnPropertyChanged();
            }
        }

        public ActivityDescription DirectoryType { get; set; }

        public ICollectionView FileSystemCollection { get; }
        public ICollectionView Tasks { get; }

        public ObservableCollection<FileSystemModel> SelectedModels { get; set; } = new();
        public ObservableCollection<MenuItemViewModel> ListViewContextMenu { get; set; } =
            new();
        #endregion

        #region Constructor
        public FileExplorerViewModel()
        {
            FileSystemCollection = CollectionViewSource.GetDefaultView(_fileSystemCollection);
            Tasks = CollectionViewSource.GetDefaultView(_tasks);

            Tasks.GroupDescriptions.Add(new PropertyGroupDescription("GroupName"));

            Init();

            OpenCommand = new DelegateCommand(OnOpen);
            GoToParentCommand = new DelegateCommand(OnGoToParent, OnCanGoToParent);

            SelectedModels.CollectionChanged += SelectedModels_CollectionChanged;

            GoToParentCommand?.RaiseCanExecuteChanged();
        }
        #endregion

        #region Commands
        public DelegateCommand OpenCommand { get; }

        public DelegateCommand GoToParentCommand { get; }


        #endregion

        #region Commands Methods
        private void OnOpen()
        {
            if (SelectedModels.Count == 1)
                ExplorerViewModel.OnOpen(SelectedModels.First());

        }
        private bool OnCanGoToParent()
        {
            return ExplorerViewModel.Navigation.Current.Path != "computer:\\";
        }
        private void OnGoToParent()
        {
            var di = new DirectoryInfo(ExplorerViewModel.Navigation.Current.Path);

            if (di.Parent == null)
            {
                ExplorerViewModel.OnOpen("computer:");
            }
            else
            {
                var parent = new DirectoryModel(di.Parent);
                ExplorerViewModel.OnOpen(parent);
            }

            GoToParentCommand?.RaiseCanExecuteChanged();

        }
        #endregion

        #region Public Methods
        public virtual void Load(string path)
        {
            GoToParentCommand?.RaiseCanExecuteChanged();
            _fileSystemCollection.Clear();
            SelectedModels.Clear();
            if (ExplorerViewModel.Navigation.Current.Name == Locale.Storage_Object_Names_Computer || path.ToLower() == "computer:\\")
            {
                CurrentModel = FileSystemModel.Create(Storage.GenericCollection.FindById(Locale.Storage_Object_Names_Computer));
                if (ExplorerViewModel.Navigation.Current.Name.ToLower() != Locale.Storage_Object_Names_Computer.ToLower()) ExplorerViewModel.Navigation.Current.Name = Locale.Storage_Object_Names_Computer;
                foreach (var obj in Storage.GenericCollection.FindAll())
                {
                    if (obj.Name != Locale.Storage_Object_Names_Computer)
                    {
                        _fileSystemCollection.Add(FileSystemModel.Create(obj, OnSelectModel));
                    }
                }
                foreach (var obj in DriveInfo.GetDrives())
                {
                    _fileSystemCollection.Add(FileSystemModel.Create(obj, OnSelectModel));
                }

                DirectoryType = ActivityDescription.ComputerFolder;
                return;
            }
            else if (ExplorerViewModel.Navigation.Current.Name == Locale.Storage_PhysicalDirectories_Names_MyMusic || path.ToLower() == $"{Environment.UserName}:\\Music".ToLower())
            {
                DirectoryType = ActivityDescription.MusicLib;
                LoadDirectoryObjects(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
            }
            else if (ExplorerViewModel.Navigation.Current.Name == Locale.Storage_PhysicalDirectories_Names_MyPictures || path.ToLower() == $"{Environment.UserName}:\\Pictures|Video\\".ToLower())
            {
                DirectoryType = ActivityDescription.PicturesVideoLib;
                LoadDirectoryObjects(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
                LoadDirectoryObjects(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
            }
            else if (ExplorerViewModel.Navigation.Current.Name == Locale.Storage_PhysicalDirectories_Names_MyPictures || path.ToLower() == $"{Environment.UserName}:\\Pictures|Video\\".ToLower())
            {
                DirectoryType = ActivityDescription.PicturesVideoLib;
                LoadDirectoryObjects(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
                LoadDirectoryObjects(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
            }
            else if (ExplorerViewModel.Navigation.Current.Name == Locale.Storage_PhysicalDirectories_Names_MyDocuments || path.ToLower() == $"{Environment.UserName}:\\Documents".ToLower())
            {
                DirectoryType = ActivityDescription.GenericFolder;
                LoadDirectoryObjects(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                LoadDirectoryObjects(Environment.GetFolderPath(Environment.SpecialFolder.Favorites));
            }
            else if (ExplorerViewModel.Navigation.Current.Name == Locale.Storage_Object_Names_Downloads || path.ToLower() == $"Computer:\\Downloads".ToLower())
            {
                DirectoryType = ActivityDescription.GenericFolder;
                LoadDirectoryObjects(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads");
            }
            else
            {
                DirectoryType = ActivityDescription.GenericFolder;
                LoadDirectoryObjects(path);
            }

            ExplorerViewModel.Navigation.Current.Path = path;
        }

        public void OnSelectModel(object sender, PropertyChangedEventArgs e)
        {

            if (sender is FileSystemModel s)
            {

                if (e.PropertyName == nameof(ISelectable.IsSelected))
                {
                    SelectedModels.Clear();
                    if ((bool)s.IsSelected) SelectedModels.Add(s);
                    else if ((bool)s.IsSelected) SelectedModels.Remove(s);
                }
            }
        }
        #endregion

        #region Private Methods
        private void LoadDirectoryObjects(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            foreach (var obj in di.EnumerateDirectories())
            {
                if (!obj.Attributes.HasFlag(FileAttributes.System)
                    || !obj.Attributes.HasFlag(FileAttributes.Hidden)
                    || !obj.Attributes.HasFlag(FileAttributes.Encrypted))
                {
                    _fileSystemCollection.Add(FileSystemModel.Create(obj, OnSelectModel));
                }
            }

            foreach (var obj in di.EnumerateFiles())
            {
                if (!obj.Attributes.HasFlag(FileAttributes.System)
                    || !obj.Attributes.HasFlag(FileAttributes.Hidden)
                    || !obj.Attributes.HasFlag(FileAttributes.Encrypted))
                {
                    _fileSystemCollection.Add(FileSystemModel.Create(obj, OnSelectModel));
                }
            }
        }
        private void SelectedModels_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ListViewContextMenu.Clear();

            if (SelectedModels.Count == 1)
            {

                ExplorerViewModel.Menu[0].Children.ToList().ForEach(delegate (MenuItemViewModel mivm)
                {
                    ListViewContextMenu.Add(mivm);
                });
            }
            else if (SelectedModels.Count == 0)
            {
                ExplorerViewModel.Menu[2].Children.ToList().ForEach(delegate (MenuItemViewModel mivm)
                {
                    ListViewContextMenu.Add(mivm);
                });
            }
        }
        private void Init()
        {
            //
            //
            this.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DirectoryType) || e.PropertyName == nameof(Path))
            {
                _tasks.Clear();
                if (Services.HelpSupport.Help_SupportService.Activities != null)
                    foreach (var activity in Storage.ActivitiesCollection.FindAll())
                    {
                        if (activity.FullName == DirectoryType.ToString())
                        {
                            _tasks.Add(new MenuItemViewModel(activity.Name, null)
                            {
                                GroupName = DirectoryType switch
                                {

                                    ActivityDescription.ComputerFolder => "System Tasks",
                                    ActivityDescription.PicturesVideoLib => "Pictures And Video Tasks",
                                    ActivityDescription.MusicLib => "Music Tasks",
                                    ActivityDescription.Generic => "File and Folder Tasks",
                                    ActivityDescription.GenericFolder => "File and Folder Tasks",
                                    _ => "File and Folder Tasks"
                                }
                            });

                        }

                    }

                foreach (var activity in Storage.ActivitiesCollection.FindAll())
                {
                    if (activity.FullName == ActivityDescription.PlaceLink.ToString())
                    {

                        _tasks.Add(new MenuItemViewModel(activity.Name, null)
                        {
                            GroupName = "Other Places",
                            Command = ExplorerViewModel.OpenCommand,
                            CommandParameter = Storage.GenericCollection.FindOne(x => x.Name == activity.Name)

                        });
                    }

                }
                

            }


        }

        #endregion
    }
}
