namespace Services.Navigation
{
    public interface INavigation : IEnumerable<NavigationObject>
    {
        /// <summary>
        /// Can return back in navigation history
        /// </summary>
        bool CanGoBack { get; }

        /// <summary>
        /// Can go forward in navigation history
        /// </summary>
        bool CanGoForward { get; }

        /// <summary>
        /// Method for return back
        /// </summary>
        void GoBack();

        /// <summary>
        /// Method for go forward
        /// </summary>
        void GoForward();

        /// <summary>
        /// Current element in navigation history
        /// </summary>
        NavigationObject Current { get; set; }

        /// <summary>
        /// Method for add element in navigation history
        /// </summary>
        void Add(string path, string name);

        /// <summary>
        /// Event which raises when history changed
        /// </summary>
        event EventHandler Navigated;
    }
}
