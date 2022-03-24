namespace Shared;

public sealed class DirectoryModel : FileSystemModel
{
    public string? ContainsIn { get; set; }

    public DirectoryModel(FileSystemInfo info) : base(info)
    {
        if (info is DirectoryInfo di &&
            !(di.Attributes.HasFlag(FileAttributes.System)
              || di.Attributes.HasFlag(FileAttributes.Encrypted))) ContainsIn = di.EnumerateFileSystemInfos().Count().ToString();
        FileSystemModelType = Shared.FileSystemModelType.Directory;
    }
}