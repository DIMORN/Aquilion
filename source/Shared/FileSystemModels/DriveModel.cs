using System.ComponentModel;
using System.IO;

namespace Shared
{
    public class DriveModel : FileSystemModel
    {
        public string Size { get; set; }
        public DriveModel(DriveInfo fileSystemInfo, PropertyChangedEventHandler selected)
        {
            Name = fileSystemInfo.Name;
            FullName = fileSystemInfo.Name;
            Extension = fileSystemInfo.DriveType.ToString().ToUpper();
            
            Size = fileSystemInfo.IsReady ? LengthLongToString(fileSystemInfo.TotalSize) : "0 B";
            PropertyChanged += selected;
        }
    }
}