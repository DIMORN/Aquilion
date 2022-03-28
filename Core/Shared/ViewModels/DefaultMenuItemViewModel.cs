namespace Shared;

public sealed class DefaultMenuItemViewModel : MenuItemViewModelBase
{
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="header"> Set header </param>
    /// <param name="command"> Set command</param>
    /// <param name="parameter"> Set command parameter</param>
    /// <param name="children"> Set children menus collection </param>
    /// <param name="checkable"> Set to can an menu item be checked </param>
    /// <param name="group"> If group = "", menu item be as check box else as radio button </param>
    /// <param name="separator"> Does it separator </param>
    public DefaultMenuItemViewModel(string header, DelegateCommand? command = null, object? commandparameter = null, IList<MenuItemViewModelBase>? children = null, bool? isseparator = false)
    {
        if(isseparator == true)
        {
            Type = MenuItemType.Separator;
            return;
        }

        Type = MenuItemType.Default;
        Header = header;
        if(command != null) if(commandparameter != null)
            {
                Command = command;
                CommandParameter = commandparameter;
            }
        if (children != null) Children = children;
    }
}
