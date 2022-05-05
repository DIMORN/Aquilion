namespace ExplorerApp.ViewModels;

internal class MainViewModel : BindableBase
{
    #region Public Properties
    public PreviewPaneViewModel PreviewPaneViewModel { get; }
    public ExplorerViewModel ExplorerViewModel { get; set; }
    public string? Title { get; set; }
    public ICollectionView Collection { get; set; }
    public bool? ToolBarsIsLocked { get; set; }
    #endregion

    #region Constructor
    public MainViewModel(ExplorerViewModel expl)
    {
        
        ExplorerViewModel = expl;
        PreviewPaneViewModel = new PreviewPaneViewModel(ExplorerViewModel.FileExplorerViewModel);
        Collection = CollectionViewSource.GetDefaultView(((FileExplorerViewModel)ExplorerViewModel.Current).FileSystemCollection);

        Title = "File Explorer";

        expl.PropertyChanged += MainViewModel_PropertyChanged;

        ExplorerViewModel.Menu[2].Children.Insert(0, new CheckableMenuItemViewModel("Preview", PreviewToggle));
        ExplorerViewModel.Menu[2].Children.Add(new CheckableMenuItemViewModel("Lock ToolBar", LockToolBarCheck));

        ((CheckableMenuItemViewModel)ExplorerViewModel.Menu[2].Children.ToList().Find(x => x.Header == "Lock ToolBar")).IsChecked = true;

    }

    #endregion

    #region Private Methods

    private void MainViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ExplorerViewModel.CurrentType))
        {
            if (ExplorerViewModel.CurrentType == ExplorerModelType.FileView)
            {
                Collection = CollectionViewSource.GetDefaultView(((FileExplorerViewModel)ExplorerViewModel.Current).FileSystemCollection);
                //Collection.Refresh();
            }
        }
    }
    private void LockToolBarCheck(object? sender, PropertyChangedEventArgs e)
    {
        ToolBarsIsLocked = (sender as CheckableMenuItemViewModel).IsChecked;
    }
    private void PreviewToggle(object? sender, PropertyChangedEventArgs e)
    {
        PreviewPaneViewModel.IsEnabled = (sender as CheckableMenuItemViewModel).IsChecked;
    }
    #endregion
}
