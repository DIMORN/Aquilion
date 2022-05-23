namespace Application.Explorer.ViewModels
{
    public interface IFilterableFileExplorer
    {

        /// <summary>
        /// Collection of files/directories/objects
        /// </summary>
        ICollectionView FileSystemCollection { get; }

        /// <summary>
        /// Collecton of selected models
        /// </summary>
        ObservableCollection<FileSystemModel> SelectedModels { get; }
    }
}
