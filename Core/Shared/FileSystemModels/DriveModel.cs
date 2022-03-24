namespace Shared;

public sealed class DriveModel : FileSystemModel
{
    public string? Size { get; set; }

    public DriveModel(DriveInfo di)
    {
        if (di.IsReady) Size = di.TotalSize.ToString();
        FileSystemModelType = Shared.FileSystemModelType.Drive;
    }
}