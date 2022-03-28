namespace Shared;

public sealed class DriveModel : FileSystemModel
{
    public string? FreeSpace { get; set; }
    public string? Size { get; set; }
    public string? Format { get; set; }

    public DriveModel(DriveInfo di)
    {
        

        Extension = (di.DriveType) switch
        {
            DriveType.Fixed => strings.DriveTypeAsExtension_Hard,

            DriveType.CDRom => strings.DriveTypeAsExtension_DVD,

            DriveType.Removable => strings.DriveTypeAsExtension_Removable,

            DriveType.Network => strings.DriveTypeAsExtension_Network, 

            _ => strings.DriveTypeAsExtension_Unknown
        };

        Name = di.IsReady ? $"{di.VolumeLabel} ({di.Name})" : $"{Extension} ({di.Name})";
        FullName = di.Name;

        Format = di.IsReady ? di.DriveFormat : "";

        if (di.IsReady)
        {
            Size = $"{Convert(di.AvailableFreeSpace)} / {Convert(di.TotalSize)}";
            FreeSpace = Convert(di.AvailableFreeSpace);
        }
        FileSystemModelType = Shared.FileSystemModelType.Drive;
    }
}