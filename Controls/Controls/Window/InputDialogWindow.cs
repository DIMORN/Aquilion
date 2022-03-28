using System.Threading.Tasks;

namespace Controls;

public partial class InputDialogWindow :Window
{
    #region Dependency Properties

    public static readonly DependencyProperty InputTextProperty = DependencyProperty.Register(
        "InputText", typeof(string), typeof(InputDialogWindow), new PropertyMetadata(default(string)));

    #endregion

    #region Public Properties
    public string InputText
    {
        get { return (string)GetValue(InputTextProperty); }
        set { SetValue(InputTextProperty, value); }
    }
    #endregion
    static InputDialogWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(InputDialogWindow), new FrameworkPropertyMetadata(typeof(InputDialogWindow)));
     
    }

    public InputDialogWindow()
    {
        this.Loaded += InputDialogWindow_Loaded;
    }

    private void InputDialogWindow_Loaded(object sender, RoutedEventArgs e)
    {
        Button button = (Button)this.Template.FindName("OKButton", this);
        button.Click += InputDialogWindow_Closed;
    }

    private void InputDialogWindow_Closed(object? sender, RoutedEventArgs e)
    {

        this.DialogResult = true;
    }

    public static string? ShowThisAsDialog()
    {
        var dialog = new InputDialogWindow();
        dialog.ShowDialog();
        if ((bool)dialog.DialogResult)
        {
            return dialog.InputText;
        }
        

        return "";
    }


}
