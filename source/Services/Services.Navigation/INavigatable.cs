namespace Services.Navigation
{
    public interface INavigatable
    {
        /// <summary>
        /// Instance of navigation history
        /// </summary>
        INavigation Navigation { get; }

        /// <summary>
        /// Instance of current object
        /// </summary>
        object Current { get; }

        /// <summary>
        /// Command for invoke GoBack method
        /// </summary>
        DelegateCommand GoBackCommand { get; }

        /// <summary>
        /// Command for invoke GoForward method
        /// </summary>
        DelegateCommand GoForwardCommand { get; }
    }
}