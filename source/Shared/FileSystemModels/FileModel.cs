using System.ComponentModel;
using System.IO;

namespace Shared
{
    public class FileModel : FileSystemModel
    {
        public string Date { get; set; }
        public string Size { get; set; }
        public FileModel(FileInfo fileSystemInfo, PropertyChangedEventHandler selected)
        {
            Name = fileSystemInfo.Name;
            FullName = fileSystemInfo.FullName;
            Extension = $"File {fileSystemInfo.Extension.ToUpper()}";
            Date = fileSystemInfo.LastWriteTimeUtc.ToString();
            Size = LengthLongToString(fileSystemInfo.Length);
            PropertyChanged += selected;
        }
    }
}