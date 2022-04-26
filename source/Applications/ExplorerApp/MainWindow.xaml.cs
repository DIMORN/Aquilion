namespace ExplorerApp;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new ViewModels.MainViewModel(new ExplorerViewModel());
    }
}
