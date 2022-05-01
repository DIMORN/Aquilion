namespace Shared.Core.Library.Menu;

public sealed class CheckableMenuItemViewModel : MenuItemViewModel
{
    public bool? IsCheckable { get; }
    public bool? IsChecked { get; set; }
    public event PropertyChangedEventHandler? Checked;
    public bool? IsSingleCheck { get; }

    public CheckableMenuItemViewModel(string header, PropertyChangedEventHandler check, bool? isSingleCheck, string? groupName)
    {
        Header = header;
        IsCheckable = true;
        IsSingleCheck = isSingleCheck;
        GroupName = groupName;
    }
    public CheckableMenuItemViewModel(string header, PropertyChangedEventHandler check)
    {
        Header = header;
        IsCheckable = true;
        IsSingleCheck = false;
    }
    public CheckableMenuItemViewModel()
    {
        IsCheckable = true;
        IsChecked = false;
    }
}
