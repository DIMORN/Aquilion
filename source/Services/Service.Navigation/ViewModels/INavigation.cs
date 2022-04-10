using System;
using System.Collections.Generic;
using Prism.Commands;

namespace Service.Navigation
{
    public interface INavigation : IEnumerable<NavigationModelBase>
    {
        void GoBack();
        void GoForward();
        bool CanGoBack { get; }
        bool CanGoForward { get; }
        NavigationModelBase Current { get; set; }
        void AddNavigationModelToHistory(NavigationModelBase navigationModelBase);

        event EventHandler NavigationHistoryChanged;
    }

    public interface INavigational
    {
        INavigation HistoryNavigation { get; set; }
        NavigationModelBase CurrentViewModel { get; set; }
        DelegateCommand GoBackCommand { get; }
        DelegateCommand GoForwardCommand { get; }
    }
}
