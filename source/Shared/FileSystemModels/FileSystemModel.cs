using System.ComponentModel;
using System.IO;
using Service.Storage;

namespace Shared
{
    public class FileSystemModel : BaseViewModel, IFileSystemModel, ISelectable
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Extension { get; set; }
        public bool IsSelected { get; set; }

        public static FileSystemModel CreateNewFileSystemModel(object fileSystemInfo, PropertyChangedEventHandler selected)
        {
            return fileSystemInfo switch
            {
                DirectoryInfo => new DirectoryModel((DirectoryInfo)fileSystemInfo, selected),
                FileInfo => new FileModel((FileInfo)fileSystemInfo, selected),
                DriveInfo => new DriveModel((DriveInfo)fileSystemInfo, selected),
                StorageObject => new StorageObjectModel((StorageObject) fileSystemInfo, selected)
            };
        }

        private static readonly string[] Sizes;

        static FileSystemModel()
        {
            Sizes = new[] { "B", "KB", "MB", "GB", "TB" };
        }

        public string LengthLongToString(long length)
        {
            double len = length;

            var order = 0;

            while (len >= 1024 && order < Sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }

            return $"{len:0.##} {Sizes[order]}";
        }
    }
}