namespace Shared.Core.Library.Explorer;

public sealed class FileExplorerViewModel : BindableBase, IFileExplorer
{
    #region Private Methods
    private readonly ExplorerViewModel _explorerViewModel;
    #endregion

    #region Public Properties
    public ExplorerViewModel ExplorerViewModel => _explorerViewModel;
    public ObservableCollection<MenuItemViewModel> ListViewContextMenu { get; set; } = 
        new ObservableCollection<MenuItemViewModel>();
    public string Path { get; set; }
    public string Name { get; set; }
    public ObservableCollection<FileSystemModel>? FileSystemCollection { get; set; } =
        new();
    public ObservableCollection<FileSystemModel>? SelectedModels { get; set; } =
        new();
    public bool? CheckableListItems { get; set; }

    public ObservableCollection<MenuItemViewModel> Tasks { get; set; } =
        new();
    #endregion

    #region Constructor
    public FileExplorerViewModel(ExplorerViewModel explorerViewModel, string? path = null, string? name = null) 
    {
        _explorerViewModel = explorerViewModel;

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
            _explorerViewModel.OnOpen(SelectedModels.First());

    }
    private bool OnCanGoToParent()
    {
        return Path != "computer:\\";
    }
    private void OnGoToParent()
    {
        var di = new DirectoryInfo(Path);
        
        if(di.Parent == null)
        {
            _explorerViewModel.OnOpen("computer:");
        }
        else
        {
            var parent = new DirectoryModel(di.Parent);
            _explorerViewModel.OnOpen(parent);
        }

        GoToParentCommand?.RaiseCanExecuteChanged();

    }
    #endregion

    #region Public Methods
    public void Load(string path)
    {
        GoToParentCommand?.RaiseCanExecuteChanged();
        FileSystemCollection.Clear();
        SelectedModels.Clear();
        if (Name == Locale.Locale.Storage_Object_Names_Computer || path.ToLower() == "computer:\\")
        {
            if (Name.ToLower() != Locale.Locale.Storage_Object_Names_Computer.ToLower()) Name = Locale.Locale.Storage_Object_Names_Computer;
            foreach (var obj in Storage.GenericCollection.FindAll())
            {
                if (obj.Name != Locale.Locale.Storage_Object_Names_Computer)
                {
                    FileSystemCollection.Add(FileSystemModel.Create(obj, OnSelectModel));
                }
            }
            foreach (var obj in DriveInfo.GetDrives())
            {
                FileSystemCollection.Add(FileSystemModel.Create(obj, OnSelectModel));
            }
            return;
        }
        else
        {
            DirectoryInfo di = new DirectoryInfo(path);

            foreach (var obj in di.EnumerateDirectories())
            {
                if (!obj.Attributes.HasFlag(FileAttributes.System)
                || !obj.Attributes.HasFlag(FileAttributes.Hidden)
                || !obj.Attributes.HasFlag(FileAttributes.Encrypted))
                {
                    FileSystemCollection.Add(FileSystemModel.Create(obj, OnSelectModel));
                }

            }
            foreach (var obj in di.EnumerateFiles())
            {
                if (!obj.Attributes.HasFlag(FileAttributes.System)
                || !obj.Attributes.HasFlag(FileAttributes.Hidden)
                || !obj.Attributes.HasFlag(FileAttributes.Encrypted))
                {
                    FileSystemCollection.Add(FileSystemModel.Create(obj, OnSelectModel));
                }
            }
        }

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
    private void SelectedModels_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        ListViewContextMenu.Clear();

        if (SelectedModels.Count == 1)
        {

            _explorerViewModel.Menu[0].Children.ToList().ForEach(delegate (MenuItemViewModel mivm)
            {
                ListViewContextMenu.Add(mivm);
            });
        }
        else if (SelectedModels.Count == 0)
        {
            _explorerViewModel.Menu[2].Children.ToList().ForEach(delegate (MenuItemViewModel mivm)
            {
                ListViewContextMenu.Add(mivm);
            });
        }
    }
    private void Init()
    {
        //
        //
    }
    #endregion
}

public interface IFileExplorer
{
    /// <summary>
    /// Collection of files/directories/objects
    /// </summary>
    ObservableCollection<FileSystemModel>? FileSystemCollection { get; set; }

    /// <summary>
    /// Collecton of selected models
    /// </summary>
    ObservableCollection<FileSystemModel>? SelectedModels { get; set; }

    /// <summary>
    /// Method for load directory
    /// </summary>
    /// <param name="path">path to directory</param>
    void Load(string path);

    /// <summary>
    /// Invokes when model in file system collectuin has been selected
    /// </summary>
    /// <param name="sender">model</param>
    /// <param name="e">args property changed</param>
    void OnSelectModel(object sender, PropertyChangedEventArgs e);
}