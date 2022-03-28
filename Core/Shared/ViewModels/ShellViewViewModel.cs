namespace Shared;

public class ShellViewViewModel : BaseViewModel
{
    #region Private Fields
    /// <summary>
    /// Field for preview size
    /// </summary>
    private double _previousPreviewSize = 250;
    #endregion

    #region Public Properties
    /// <summary>
    /// Boolean for toggle preview
    /// </summary>
    public bool? Preview { get; set; }
    /// <summary>
    /// Preview pane size property
    /// </summary>
    public double? PreviewSize
    {
        get
        {
            if (Preview == true)
            {
                return _previousPreviewSize;
            }
            else return 0;
        }
        set
        {
            if (value > 40)
            {
                _previousPreviewSize = (double)value;
            }
        }
    }
    /// <summary>
    /// Maximum items count in controlable collection
    /// </summary>
    public int? MaximumItemsCount { get; set; }
    /// <summary>
    /// Index of selected item, starts with 1
    /// </summary>
    public int? SelectedItem { get; set; }
    #endregion

    #region Constructor
    public ShellViewViewModel()
    {
        this.PropertyChanged += ShellViewViewModel_PropertyChanged;

        IncreaseSelectedItemIndexCommand = new DelegateCommand(OnIncrease, OnCanIncrease);
        DecreaseSelectedItemIndexCommand = new DelegateCommand(OnDecreasing, OnCanDecrease);
    }

    private void ShellViewViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if(e.PropertyName == nameof(SelectedItem))
        {
            if (SelectedItem == 0) SelectedItem++;
        }
    }
    #endregion

    #region Commands
    /// <summary>
    /// Increasing selected item index
    /// </summary>
    public DelegateCommand? IncreaseSelectedItemIndexCommand { get; }
    /// <summary>
    /// Decreasing selected item index
    /// </summary>
    public DelegateCommand? DecreaseSelectedItemIndexCommand { get; }
    #endregion

    #region Commands Methods
    private bool OnCanIncrease(object arg)
    {
        return MaximumItemsCount != 0;
    }
    private void OnIncrease (object obj)
    {
        SelectedItem = SelectedItem == MaximumItemsCount ? 0 : SelectedItem++;
    }
    private bool OnCanDecrease(object arg)
    {
        return MaximumItemsCount != 0;
    }
    private void OnDecreasing(object obj)
    {
        SelectedItem = SelectedItem == 0 ? MaximumItemsCount : SelectedItem--;
    }
    #endregion
}
