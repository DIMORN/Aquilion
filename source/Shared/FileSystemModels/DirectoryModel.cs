using System.ComponentModel;
using System.IO;

namespace Shared
{
    public class DirectoryModel : FileSystemModel
    {
        public string Date { get; set; }
        public DirectoryModel(DirectoryInfo fileSystemInfo, PropertyChangedEventHandler selected)
        {
            Name = fileSystemInfo.Name;
            FullName = fileSystemInfo.FullName;
            Extension = "File Folder";
            Date = fileSystemInfo.LastWriteTimeUtc.ToString();
            PropertyChanged += selected;
        }
    }
}