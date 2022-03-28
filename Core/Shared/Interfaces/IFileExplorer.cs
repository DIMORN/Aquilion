namespace Shared;

public interface IFileExplorer
{
    #region Public Properties
    public string? CurrentName { get; set; }
    public string? CurrentPath { get; set; }
    public ObservableCollection<IFileSystemModel>? FileSystemModelsCollection { get; set; }

    #endregion

    #region Commands

    public DelegateCommand? OpenFileSystemModelCommand { get; }
    
    public DelegateCommand? GoParentCommand { get; }

    #endregion

}