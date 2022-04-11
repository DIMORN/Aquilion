using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Prism.Commands;
using Service.Navigation;
using Service.Storage;
using Shared;
using BaseViewModel = Shared.BaseViewModel;

namespace Explorer.Common
{
    public class ShellViewViewModel : NavigationModelBase
    {
        public object CurrentPresenter { get; set; }
    }
    public class FileExplorerViewModel : ShellViewViewModel
    {
        public ExplorerViewModel ExplorerViewModel { get; }
        public ICollectionView Collection { get; set; }

        public ObservableCollection<IFileSystemModel> FileListviewCollection { get; set; } =
            new ObservableCollection<IFileSystemModel>();
        public ObservableCollection<IFileSystemModel> SelectedFileSystemModels { get; set; } =
            new ObservableCollection<IFileSystemModel>();

        public FileExplorerViewModel(ExplorerViewModel explorerViewModel)
        {
            ExplorerViewModel = explorerViewModel;
            Collection = CollectionViewSource.GetDefaultView(FileListviewCollection);
        }

        #region Commands

        public DelegateCommand GoToParentCommand { get; }

        #endregion

        public void LoadDirectory(string path)
        {
            if (path == @"computer:\")
            {
                FileListviewCollection.Clear();
                SelectedFileSystemModels.Clear();
                var c = Storage.GenericObjectsCollection.FindAll();
                foreach (var storageObject in c)
                {
                    FileListviewCollection.Add(FileSystemModel.CreateNewFileSystemModel(storageObject, ListviewCollectionItemSelected));
                }

                foreach (var driveInfo in DriveInfo.GetDrives())
                {
                    FileListviewCollection.Add(FileSystemModel.CreateNewFileSystemModel(driveInfo, ListviewCollectionItemSelected));
                }
            }
            else
            {
                FileListviewCollection.Clear();
                SelectedFileSystemModels.Clear();

                foreach (var enumerateFileSystemInfo in new DirectoryInfo(path).EnumerateFileSystemInfos())
                {
                    Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        FileListviewCollection.Add(FileSystemModel.CreateNewFileSystemModel(enumerateFileSystemInfo, ListviewCollectionItemSelected));
                    });
                }
            }
        }

        private void ListviewCollectionItemSelected(object sender, PropertyChangedEventArgs e)
        {
            var selectModel = sender as FileSystemModel;

            if (selectModel.IsSelected) SelectedFileSystemModels.Add(selectModel);
            else SelectedFileSystemModels.Remove(selectModel);
        }
    }
}