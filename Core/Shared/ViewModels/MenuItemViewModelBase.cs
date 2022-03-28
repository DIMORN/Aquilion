namespace Shared;

public class MenuItemViewModelBase : BaseViewModel
{
    /// <summary>
    /// Header of menu item
    /// </summary>
    public string? Header { get; set; }

    /// <summary>
    /// Command of menu item
    /// </summary>
    public DelegateCommand? Command { get; set; }

    /// <summary>
    /// Command parameter for command of menu item
    /// </summary>
    public object? CommandParameter { get; set; }

    /// <summary>
    /// Type of menu item
    /// </summary>
    public MenuItemType? Type { get; set; }

    /// <summary>
    /// Collection of children menu item
    /// </summary>
    public IList<MenuItemViewModelBase>? Children { get; set; }

    /// <summary>
    /// Does it separator menu item
    /// </summary>
    public bool? IsSeparator { get; set; }
}

