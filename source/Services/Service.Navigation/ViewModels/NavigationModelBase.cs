namespace Service.Navigation
{
    public class NavigationModelBase
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public NavigationModelBase Previous { get; set; }
        public NavigationModelBase Next { get; set; }

        public NavigationModelBase(NavigationModelBase navigationModelBase = null)
        {
            if (navigationModelBase != null)
            {
                Name = navigationModelBase.Name;
                FullName = navigationModelBase.FullName;
            }
        }
    }
}