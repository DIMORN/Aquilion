namespace ExplorerApp.ViewModels;

internal class MainViewModel
{
    public ExplorerViewModel ExplorerViewModel { get; set; }
    public string? Title { get; set; }
    public ICollectionView Collection { get; set; }
    public bool? ToolBarsIsLocked { get; set; }
    public MainViewModel(ExplorerViewModel expl)
    {
        ExplorerViewModel = expl;
        Collection = CollectionViewSource.GetDefaultView(((FileExplorerViewModel)ExplorerViewModel.Current).FileSystemCollection);

        Title = "File Explorer";

        expl.PropertyChanged += MainViewModel_PropertyChanged;

        ExplorerViewModel.Menu[2].Children.Add(new CheckableMenuItemViewModel("Lock ToolBar", LockToolBarCheck));

        ((CheckableMenuItemViewModel)ExplorerViewModel.Menu[2].Children.ToList().Find(x => x.Header == "Lock ToolBar")).IsChecked = true;
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
    private void LockToolBarCheck(object? sender, PropertyChangedEventArgs e)
    {
        ToolBarsIsLocked = ((CheckableMenuItemViewModel)ExplorerViewModel.Menu[2].Children.ToList().Find(x => x.Header == "Lock ToolBar")).IsChecked;
    }
}
