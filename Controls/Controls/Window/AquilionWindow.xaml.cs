using System.Diagnostics.Tracing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Controls;

public partial class AquilionWindow : Window
{
    #region Private Fields
    

    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
    {
        if ((msg == WM_SYSTEMMENU) && (wparam.ToInt32() == WP_SYSTEMMENU))
        {
            handled = false;
        }
        return IntPtr.Zero;
    }

    #endregion

    #region Private Constants

    
    private const uint WP_SYSTEMMENU = 0x02;
    private const uint WM_SYSTEMMENU = 0xa4;
    
    private const string Draggable = "PART_Draggable";

    #endregion

    #region Dependency Properties

    public static readonly DependencyProperty ToolBarProperty = DependencyProperty.Register(
        "ToolBar", typeof(UIElement), typeof(AquilionWindow), new PropertyMetadata(default(UIElement)));

    #endregion

    #region Public Properties
    public UIElement ToolBar
    {
        get { return (UIElement)GetValue(ToolBarProperty); }
        set { SetValue(ToolBarProperty, value); }
    }
    public DelegateCommand MinimizeCommand { get; }
    public DelegateCommand MaximizeCommand { get; }
    public DelegateCommand CloseCommand { get; }
    #endregion

    static AquilionWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AquilionWindow), new FrameworkPropertyMetadata(typeof(AquilionWindow)));
    }
    public AquilionWindow()
    {
        InitializeComponent();

        AllowsTransparency = true;
        WindowStyle = WindowStyle.None;
        ResizeMode = ResizeMode.CanResizeWithGrip;

        this.Loaded += OnLoaded;
        
        MinimizeCommand = new DelegateCommand(OnMinimize);
        MaximizeCommand = new DelegateCommand(OnMaximize);
        CloseCommand = new DelegateCommand(OnClose);
    }

    
    #region WindowBasicMethods
    public void OnMaximize(object obj)
    {
        if (WindowState == WindowState.Maximized)
            SystemCommands.RestoreWindow(this);
        else SystemCommands.MaximizeWindow(this);
    }

    public void OnClose(object obj) => SystemCommands.CloseWindow(this);

    public void OnMinimize(object obj) => SystemCommands.MinimizeWindow(this);
    private void DraggableBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if(this.WindowState == WindowState.Maximized)
        {
            this.WindowState = WindowState.Normal;
            this.Left = Mouse.GetPosition(this).X;
            this.Top = Mouse.GetPosition(this).Y;
        }
        
        this.DragMove();
    }
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        IntPtr windIntPtr = new WindowInteropHelper(this).Handle;
        HwndSource hwndSource = HwndSource.FromHwnd(windIntPtr);
        hwndSource.AddHook(new HwndSourceHook(WndProc));
    }


    #endregion

    #region Public Methods

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
        this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

        var DraggableBorder = (Border)this.Template.FindName(Draggable, this);
        DraggableBorder.MouseLeftButtonDown += DraggableBorder_MouseLeftButtonDown;
    }

    #endregion
}