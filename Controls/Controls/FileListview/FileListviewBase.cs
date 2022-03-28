namespace Controls;

public partial class FileListviewBase : UserControl
{
    public static readonly DependencyProperty ViewProperty = DependencyProperty.Register(
        "View", typeof(string), typeof(FileListviewBase), new PropertyMetadata(default(string)));

    public string View
    {
        get { return (string) GetValue(ViewProperty); }
        set { SetValue(ViewProperty, value); }
    }
    
    static FileListviewBase()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(FileListviewBase), new FrameworkPropertyMetadata(typeof(FileListviewBase)));
    }
}