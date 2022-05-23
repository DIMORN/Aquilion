namespace Shared.Core.Library.Menu
{
    public sealed class CheckableMenuItemViewModel : MenuItemViewModel
{
    public bool? IsCheckable { get; }
    public bool? IsChecked { get; set; }
    public bool? IsSingleCheck { get; }

    public CheckableMenuItemViewModel(string header, PropertyChangedEventHandler check, bool? isSingleCheck, string? groupName)
    {
        Header = header;
        IsCheckable = true;
        IsSingleCheck = isSingleCheck;
        GroupName = groupName;
        PropertyChanged += check;
    }
    public CheckableMenuItemViewModel(string header, PropertyChangedEventHandler check)
    {
        Header = header;
        IsCheckable = true;
        IsSingleCheck = false;
        PropertyChanged += check;
    }
}

}

