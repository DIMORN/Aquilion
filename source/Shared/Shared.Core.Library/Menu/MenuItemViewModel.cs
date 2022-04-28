namespace Shared.Core.Library.Menu;

public class MenuItemViewModel : BindableBase
{
    public string? Header { get; set; }
    public DelegateCommand<object>? Command { get; set; }
    public object? CommandParameter { get; set; }
    public IList<MenuItemViewModel>? Children { get; set; }
    public string ? GroupName { get; set; }

    public MenuItemViewModel(string header, IList<MenuItemViewModel> children)
    {
        Header = header;
        Children = children;
    }
    public MenuItemViewModel(string header, DelegateCommand<object> command, object? parameter = null)
    {
        Header = header;
        Command = command;
        CommandParameter = parameter;
    }
    public MenuItemViewModel()
    {

    }
}
