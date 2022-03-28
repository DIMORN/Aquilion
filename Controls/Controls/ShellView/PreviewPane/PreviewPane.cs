namespace Controls;

public partial class PreviewPane : UserControl
{
    #region Dependency Properties

    public static readonly DependencyProperty MaximumItemsProperty = DependencyProperty.Register(
        "MaximumItems", typeof(int), typeof(PreviewPane), new PropertyMetadata(default(int)));

    public static readonly DependencyProperty SelectedProperty = DependencyProperty.Register(
        "Selected", typeof(IEnumerable<object>), typeof(PreviewPane), new PropertyMetadata(default(IEnumerable<object>)));

    public static readonly DependencyProperty CurrentModeProperty = DependencyProperty.Register(
        "CurrentMode", typeof(string), typeof(PreviewPane), new PropertyMetadata(default(string)));

    #endregion

    #region Public Properties
    public int MaximumItems
    {
        get { return (int)GetValue(MaximumItemsProperty); }
        set { SetValue(MaximumItemsProperty, value); }
    }
    public IEnumerable<object> Selected
    {
        get { return (IEnumerable<object>)GetValue(SelectedProperty); }
        set { SetValue(SelectedProperty, value); }
    }
    public string CurrentMode
    {
        get { return (string)GetValue(CurrentModeProperty); }
        set { SetValue(CurrentModeProperty, value); }
    }
    #endregion
    static PreviewPane()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PreviewPane), new FrameworkPropertyMetadata(typeof(PreviewPane)));
    }
}
