using System.Collections.Generic;
using Prism.Commands;

namespace Shared
{
    public class MenuItemViewModel : BaseViewModel
    {
        public string Header { get; set; }
        public DelegateCommand<object> Command { get; set; }
        public object Parameter { get; set; }
        public IList<MenuItemViewModel> Children { get; set; }

        public MenuItemViewModel(
            string header,
            DelegateCommand<object> command = null,
            object parameter = null,
            IList<MenuItemViewModel> children = null)
        {
            Header = header;
            Command = command;
            Parameter = parameter;
            Children = children;
        }
    }
}
