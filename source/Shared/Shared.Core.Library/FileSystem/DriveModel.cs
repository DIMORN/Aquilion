namespace Shared.Core.Library.FileSystem
{
    public class DriveModel : FileSystemModel
{
    public string? Size { get; set; }
    public long TotalFreeSpace { get; }
    public long TotalSize { get; }
    public double UsedPercentage { get; set; }
    public DriveModel(DriveInfo info, PropertyChangedEventHandler? selected = null)
    {
        string drivetype = "";
        if (info.IsReady)
        {
            if (String.IsNullOrEmpty(info.VolumeLabel))
            {
                
                switch (info.DriveType)
                {
                    case DriveType.Fixed:
                        drivetype = Locale.Locale.FileSystem_Extension_Drive_HDD;
                        break;
                    case DriveType.CDRom:
                        drivetype = Locale.Locale.FileSystem_Extension_Drive_CD_DVD;
                        break;
                    case DriveType.Removable:
                        drivetype = Locale.Locale.FileSystem_Extension_Drive_REMOVABLE;
                        break;
                    case DriveType.Network:
                        drivetype = Locale.Locale.FileSystem_Extension_Drive_Network;
                        break;
                    case DriveType.Unknown:
                        drivetype = Locale.Locale.FileSystem_Extension_Drive_Unknown;
                        break;
                    default:
                        drivetype = Locale.Locale.FileSystem_Extension_Drive_Unknown;
                        break;
                }
                Name = $"{drivetype} ({info.Name})";
            }
            else
            {
                Name = $"{info.VolumeLabel} ({info.Name})";
            }
            TotalSize = info.TotalSize;
            TotalFreeSpace = info.TotalFreeSpace;

            UsedPercentage = (TotalSize - TotalFreeSpace) / (double)TotalSize * 100;

            Size = $"{LengthToString(TotalFreeSpace)} / {LengthToString(TotalSize)}";
        }
        else
        {
            switch (info.DriveType)
            {
                case DriveType.Fixed:
                    drivetype = Locale.Locale.FileSystem_Extension_Drive_HDD;
                    break;
                case DriveType.CDRom:
                    drivetype = Locale.Locale.FileSystem_Extension_Drive_CD_DVD;
                    break;
                case DriveType.Removable:
                    drivetype = Locale.Locale.FileSystem_Extension_Drive_REMOVABLE;
                    break;
                case DriveType.Network:
                    drivetype = Locale.Locale.FileSystem_Extension_Drive_Network;
                    break;
                case DriveType.Unknown:
                    drivetype = Locale.Locale.FileSystem_Extension_Drive_Unknown;
                    break;
                default:
                    drivetype = Locale.Locale.FileSystem_Extension_Drive_Unknown;
                    break;
            }
            Name = $"{drivetype} ({info.Name})";
        }

        Extension = info.DriveType switch
        {
            DriveType.Fixed => Locale.Locale.FileSystem_Extension_Drive_HDD,
            DriveType.CDRom => Locale.Locale.FileSystem_Extension_Drive_CD_DVD,
            DriveType.Removable => Locale.Locale.FileSystem_Extension_Drive_REMOVABLE,
            DriveType.Network => Locale.Locale.FileSystem_Extension_Drive_Network,

            DriveType.Unknown => Locale.Locale.FileSystem_Extension_Drive_Unknown,
            _ => Locale.Locale.FileSystem_Extension_Drive_Unknown
        };

        FullName = info.Name;

        IsSelected = false;

        PropertyChanged += selected;
    }
}

}


