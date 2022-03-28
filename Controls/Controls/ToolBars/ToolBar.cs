namespace Controls;

public partial class ToolBar : UserControl
{
    #region Dependency Properties

    public static readonly DependencyProperty MainMenuProperty = DependencyProperty.Register(
        "MainMenu", typeof(IEnumerable<object>), typeof(ToolBar), new PropertyMetadata(default(IEnumerable<object>)));

    #endregion

    #region Public Properties
    public IEnumerable<object> MainMenu
    {
        get { return (IEnumerable<object>)GetValue(MainMenuProperty); }
        set { SetValue(MainMenuProperty, value); }
    }
    #endregion

    static ToolBar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ToolBar), new FrameworkPropertyMetadata(typeof(ToolBar)));
    }
}

