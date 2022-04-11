using System.Collections.Generic;
using System.ComponentModel;
using Prism.Commands;

namespace Shared
{
    public class CheckableMenuItem : MenuItemViewModel
    {
        public string GroupName { get; set; }
        public bool IsChecked { get; set; }
        public CheckableMenuItem(string header, PropertyChangedEventHandler check, string group, DelegateCommand<object> command = null, object parameter = null, IList<MenuItemViewModel> children = null) : base(header, command, parameter, children)
        {
            Header = header;
            GroupName = group;
            this.PropertyChanged += check;
        }
    }
}