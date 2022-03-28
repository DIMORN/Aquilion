namespace Shared;

public class CheckableMenuItemViewModel : MenuItemViewModelBase
{
    /// <summary>
    /// Does menu item chackable
    /// </summary>
    public bool? IsCheckable { get; set; }
    /// <summary>
    /// Does menu item checked
    /// </summary>
    public bool? IsChecked { get; set; }
    public CheckableMenuItemViewModel(string header)
    {
        IsCheckable = true;
        Type = MenuItemType.Checkable;
        Header = header;
    }
}
