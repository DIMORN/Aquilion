namespace Shared;

public class ExplorerVM : BaseViewModel, IFileExplorer, INavigable
{
    #region Public Properties

    public string? CurrentName { get; set; }
    public string? CurrentPath { get; set; }
    public ObservableCollection<string> FileSystemModelsCollection { get; set; }

    #endregion

    #region Constructor

    public ExplorerVM()
    {
        CurrentName = "my Computer";
        CurrentPath = @"my Computer:\";
    }

    #endregion

    #region Commands

    public DelegateCommand? OpenFileSystemModelCommand { get; }
    public DelegateCommand? GoBackCommand { get; }
    public DelegateCommand? GoForwardCommand { get; }
    public DelegateCommand? GoParentCommand { get; }

    #endregion
}