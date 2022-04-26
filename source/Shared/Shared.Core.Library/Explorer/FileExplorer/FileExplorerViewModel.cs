namespace Shared.Core.Library.Explorer;

public sealed class FileExplorerViewModel : NavigationObject, IFileExplorer
{
    #region Public Properties
    public ObservableCollection<FileSystemModel>? FileSystemCollection { get; set; } =
        new();
    public ObservableCollection<FileSystemModel>? SelectedModels { get; set; } =
        new();
    #endregion

    #region Constructor
    public FileExplorerViewModel(string? path = null, string? name = null) : base(path, name)
    {
        
    }

    

    public void Load(string path)
    {
        FileSystemCollection.Clear();
        SelectedModels.Clear();
        if (Name == Locale.Locale.Storage_Object_Names_Computer)
        {
            foreach(var obj in Storage.GenericCollection.FindAll())
            {
                if(obj.Name != Locale.Locale.Storage_Object_Names_Computer)
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
        SelectedModels.Clear();
        if (sender is FileSystemModel s)
        {
            if(e.PropertyName == nameof(ISelectable.IsSelected))
            {
                if ((bool)s.IsSelected) SelectedModels.Add(s);
                else if ((bool)s.IsSelected) SelectedModels.Remove(s);
            }
        }
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