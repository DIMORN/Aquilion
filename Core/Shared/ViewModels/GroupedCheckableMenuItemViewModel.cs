namespace Shared;

public sealed class GroupedCheckableMenuItemViewModel : CheckableMenuItemViewModel
{
    /// <summary>
    /// If group has name, menu item be as radio button
    /// </summary>
    public string? GroupName { get; set; }
    public GroupedCheckableMenuItemViewModel(string header, string group) : base(header)
    {
        IsCheckable = true;
        GroupName = group;
        Type = MenuItemType.GroupedCheckable;
        Header = header;
    }
}
