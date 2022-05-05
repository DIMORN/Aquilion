namespace ExplorerApp.ViewModels;

public class PreviewPaneViewModel : BindableBase
{
    #region Private Fields
    private FileExplorerViewModel _fileExplorerViewModel;

    private double _oldPreviewHeight;
    #endregion

    #region Public Properties
    public bool? IsEnabled { get; set; }
    public ObservableCollection<FileSystemModel> Selected => _fileExplorerViewModel.SelectedModels;
    public GridLength? PreviewHeight
    {
        get
        {
            return (bool)IsEnabled ? new GridLength(_oldPreviewHeight) : new GridLength(0);
        }
        set
        {
            if (value.Value.Value > 40 && value.Value.Value < 500)
                _oldPreviewHeight = value.Value.Value;
        }
    }
    public string? Title { get; set; }
    public string? IteratorText { get; set; }
    public int? SelectedIndex { get; set; }
    public int? Maximum => _fileExplorerViewModel.FileSystemCollection.Count();
    #endregion

    #region Constructor
    public PreviewPaneViewModel(FileExplorerViewModel fileExplorerViewModel)
    {
        _fileExplorerViewModel = fileExplorerViewModel;
        _oldPreviewHeight = 210;
        IsEnabled = true;
        ((CheckableMenuItemViewModel)fileExplorerViewModel.ExplorerViewModel.Menu[2].Children[0]).IsChecked = true;
        _fileExplorerViewModel.SelectedModels.CollectionChanged += SelectedModels_CollectionChanged;
        this.PropertyChanged += PreviewPaneViewModel_PropertyChanged;
    }

    #endregion

    #region Private Methods
    private void SelectedModels_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if(Selected.Count == 0)
        {
            Title = "No items selected.";
            IteratorText = $"";
        }
        else if (Selected.Count == 1)
        {
            Title = Selected[0].Name;
            IteratorText = $"{SelectedIndex + 1} of {Maximum}";
        }
        else if (Selected.Count > 1)
        {
            Title = $"{Selected.Count} items selected.";
            IteratorText = $"{SelectedIndex + 1} of {Maximum}";
        }
    }

    private void PreviewPaneViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (Selected.Count == 0)
        {
            Title = "No items selected.";
            IteratorText = $"";
        }
        else if (Selected.Count == 1)
        {
            Title = Selected[0].Name;
            IteratorText = $"{SelectedIndex + 1} of {Maximum}";
        }
        else if (Selected.Count > 1)
        {
            Title = $"{Selected.Count} items selected.";
            IteratorText = $"{SelectedIndex + 1} of {Maximum}";
        }
    }
    #endregion
}
