namespace Shared;

public sealed class FileModel : FileSystemModel
{
    public string? Size { get; set; }
    public FileModel(FileSystemInfo info) : base(info)
    {
        if (info is FileInfo fi) Size = fi.Length.ToString();
        FileSystemModelType = Shared.FileSystemModelType.File;
    }
}