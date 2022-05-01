namespace Shared.Core.Library.FileSystem;

public class DriveModel : FileSystemModel
{
    public string? Size { get; set; }
    public long TotalFreeSpace { get; }
    public long TotalSize { get; }
    public double UsedPercentage { get; set; }
    public DriveModel(DriveInfo info, PropertyChangedEventHandler? selected = null)
    {
        
        if (info.IsReady)
        {
            if (String.IsNullOrEmpty(info.VolumeLabel))
            {
                Name = $"{info.DriveType switch
                {
                    DriveType.Fixed => Locale.Locale.FileSystem_Extension_Drive_HDD,
                    DriveType.CDRom => Locale.Locale.FileSystem_Extension_Drive_CD_DVD,
                    DriveType.Removable => Locale.Locale.FileSystem_Extension_Drive_REMOVABLE,
                    DriveType.Network => Locale.Locale.FileSystem_Extension_Drive_Network,

                    DriveType.Unknown => Locale.Locale.FileSystem_Extension_Drive_Unknown,
                    _ => Locale.Locale.FileSystem_Extension_Drive_Unknown
                }} ({info.Name})";
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
            Name = $"{info.DriveType switch 
            {
                DriveType.Fixed => Locale.Locale.FileSystem_Extension_Drive_HDD,
                DriveType.CDRom => Locale.Locale.FileSystem_Extension_Drive_CD_DVD,
                DriveType.Removable => Locale.Locale.FileSystem_Extension_Drive_REMOVABLE,
                DriveType.Network => Locale.Locale.FileSystem_Extension_Drive_Network,

                DriveType.Unknown => Locale.Locale.FileSystem_Extension_Drive_Unknown,
                _ => Locale.Locale.FileSystem_Extension_Drive_Unknown
            }} ({info.Name})";
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

