using System;
using System.Collections.ObjectModel;
using Prism.Commands;
using Service.Navigation;
using Service.Storage;
using Shared;
using BaseViewModel = Shared.BaseViewModel;

namespace Explorer.Common
{
    public class ExplorerViewModel : BaseViewModel, INavigational
    {
        public INavigation HistoryNavigation { get; set; }
        public NavigationModelBase CurrentViewModel { get; set; }

        public ExplorerViewModel()
        {
            HistoryNavigation = new NavigationHistory(new FileExplorerViewModel(this)
            {
                Name = "My Computer",
                FullName = @"computer:\"
            });
            CurrentViewModel = HistoryNavigation.Current;
            Load(CurrentViewModel.FullName);

            HistoryNavigation.NavigationHistoryChanged += HistoryNavigationOnNavigationHistoryChanged;
        }

        #region Commands

        public DelegateCommand OpenCommand { get; }
        public DelegateCommand GoBackCommand { get; }
        public DelegateCommand GoForwardCommand { get; }

        #endregion

        #region Commands Methods
        private bool OnCamGoForward() => HistoryNavigation.CanGoForward;
        private bool OnCanGoBack() => HistoryNavigation.CanGoBack;
        private void OnGoForward()
        {
            HistoryNavigation.GoForward();

            CurrentViewModel = HistoryNavigation.Current;

            Load(CurrentViewModel.FullName);
        }
        private void OnGoBack()
        {
            HistoryNavigation.GoBack();

            CurrentViewModel = HistoryNavigation.Current;

            Load(CurrentViewModel.FullName);
        }

        #endregion

        #region Public Methods
        public void LoadLinkOrFileSystemModel(object obj)
        {
            if (obj is ObservableCollection<IFileSystemModel> collection)
            {
                if(collection.Count == 1)
                    if(collection[0] is DirectoryModel directoryModel)
                        HistoryNavigation.AddNavigationModelToHistory(new FileExplorerViewModel(this)
                        {
                            Name = directoryModel.Name,
                            FullName = directoryModel.FullName
                        });
            }
        }

        #endregion

        #region Private Methods
        private void Load(string path)
        {
            if (!path.StartsWith("https://")) ((FileExplorerViewModel)CurrentViewModel).LoadDirectory(path);
        }
        private void HistoryNavigationOnNavigationHistoryChanged(object? sender, EventArgs e)
        {
            GoBackCommand.RaiseCanExecuteChanged();
            GoForwardCommand.RaiseCanExecuteChanged();
        }


        #endregion
    }
}
