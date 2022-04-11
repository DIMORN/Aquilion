using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Prism.Commands;
using Service.Navigation;
using Service.Plugins;
using Service.Storage;
using Shared;
using BaseViewModel = Shared.BaseViewModel;

namespace Explorer.Common
{
    public class ExplorerViewModel : BaseViewModel, INavigational
    {
        public ObservableCollection<MenuItemViewModel> Menu { get; set; } =
            new ObservableCollection<MenuItemViewModel>();
        public INavigation HistoryNavigation { get; set; }
        public NavigationModelBase CurrentViewModel { get; set; }

        public ExplorerViewModel()
        {
            InitializeMenuItems();
            

            HistoryNavigation = new NavigationHistory(new FileExplorerViewModel(this)
            {
                Name = "My Computer",
                FullName = @"computer:\"
            });
            CurrentViewModel = HistoryNavigation.Current;
            Load(CurrentViewModel.FullName);
            InitializePlugins();
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
        private void InitializeMenuItems()
        {
            Menu.Add(new MenuItemViewModel(
                "File",
                null,
                null,
                new List<MenuItemViewModel>
                {
                    new MenuItemViewModel("Open"),
                    new MenuItemViewModel("Rename"),
                    new MenuItemViewModel("Close")
                }));
            Menu.Add(new MenuItemViewModel(
                    "Edit",
                    null,
                    null,
                    new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel("Cut"),
                        new MenuItemViewModel("Copy"),
                        new MenuItemViewModel("Paste"),
                    }));
            Menu.Add(new MenuItemViewModel(
                "View",
                null,
                null,
                new List<MenuItemViewModel>()));
            Menu.Add(new MenuItemViewModel(
                "Favorites",
                null,
                null,
                new List<MenuItemViewModel>()));
            Menu.Add(new MenuItemViewModel(
                "Tools",
                null,
                null,
                new List<MenuItemViewModel>
                {
                    new MenuItemViewModel("Folder Options")
                }));
            Menu.Add(new MenuItemViewModel(
                "Help",
                null,
                null,
                new List<MenuItemViewModel>
                {
                    new MenuItemViewModel("About")
                }));
        }
        private void InitializePlugins()
        {
            foreach (var plugin in PluginService.Plugins)
            {
                if(plugin.Key.ToLower().Contains("listview.presenter")) 
                    Menu[2].Children.Add(
                        new CheckableMenuItem(
                            plugin.Key.Split(".").Last(),
                            Check,
                            "Presenters"));
            }
            ((CheckableMenuItem)Menu[2].Children.ToList().FindAll(x => ((CheckableMenuItem)x).GroupName == "Presenters").First()).IsChecked = true;
        }

        private void Check(object sender, PropertyChangedEventArgs e)
        {
            if (CurrentViewModel is ShellViewViewModel shellViewViewModel)
            {
                if(sender is CheckableMenuItem checkableMenuItem)
                    if(checkableMenuItem.GroupName.ToLower().Contains("presenters"))
                        if (checkableMenuItem.IsChecked)
                        {
                            var rd = new ResourceDictionary
                            {
                                Source = new Uri(
                                    $"pack://application:,,,/{PluginService.Plugins.Find(x => x.Key.ToLower().Contains(checkableMenuItem.Header.ToLower())).Assembly.FullName};component/Generic.xaml")
                            };
                            shellViewViewModel.CurrentPresenter = (ControlTemplate) rd[checkableMenuItem.Header];
                        }
            }
        }

        #endregion
    }
}
