namespace Shared.Core.Library.Menu
{
    public class MenuItemViewModel : BaseViewModel
    {
        private string? _header;

        public string? Header
        {
            get => _header;
            set
            {
                _header = value;
                OnPropertyChanged();
            }
        }
        public DelegateCommandBase? Command { get; set; }
        public object? CommandParameter { get; set; }
        public IList<MenuItemViewModel>? Children { get; set; }
        public string? GroupName { get; set; }
        public object? Icon { get; set; }

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

}

