namespace ExplorerApp.ViewModels;

internal class MainViewModel
{
    public ExplorerViewModel ExplorerViewModel { get; set; }
    public string? Title { get; set; }
    public ICollectionView Collection { get; set; }
    public MainViewModel(ExplorerViewModel expl)
    {
        ExplorerViewModel = expl;
        Collection = CollectionViewSource.GetDefaultView(((FileExplorerViewModel)ExplorerViewModel.Current).FileSystemCollection);

        Title = "File Explorer";

        expl.PropertyChanged += MainViewModel_PropertyChanged;
    }

    private void MainViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if(e.PropertyName == nameof(ExplorerViewModel.CurrentType))
        {
            if(ExplorerViewModel.CurrentType == ExplorerModelType.FileView)
            {
                Collection = CollectionViewSource.GetDefaultView(((FileExplorerViewModel)ExplorerViewModel.Current).FileSystemCollection);
            }
        }
    }
}
