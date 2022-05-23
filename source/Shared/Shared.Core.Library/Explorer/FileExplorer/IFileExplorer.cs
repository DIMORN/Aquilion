namespace Shared.Core.Library.Explorer
{
    public interface IFileExplorer
    {
        /// <summary>
        /// Link to main explorer view model
        /// </summary>
        ExplorerViewModel ExplorerViewModel { get; set; }

        /// <summary>
        /// FileSystemCollection
        /// </summary>
        ObservableCollection<FileSystemModel>? _fileSystemCollection { get; set; }

        /// <summary>
        /// Collection of tasks
        /// </summary>
        ObservableCollection<MenuItemViewModel>? _tasks { get; set; }

        /// <summary>
        /// Collection of seleected models
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

        bool IsCheckableItems { get; set; }

        /// <summary>
        /// Command for invoke OnOpen method
        /// </summary>
        DelegateCommand OpenCommand { get; }
    }
}
