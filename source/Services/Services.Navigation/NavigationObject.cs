namespace Services.Navigation
{
    public class NavigationObject : BaseViewModel
    {
        private string? _name;

        public NavigationObject? PREVIOUS { get; set; }
        public NavigationObject? NEXT { get; set; }

        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string? Path { get; set; }
        public NavigationObject(string path, string name)
        {
            Name = name;
            Path = path;
        }
    }
}
