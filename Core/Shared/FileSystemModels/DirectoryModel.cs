namespace Shared;

public sealed class DirectoryModel : FileSystemModel
{
    public string? ContainsIn { get; set; }

    public DirectoryModel(FileSystemInfo info)
    {
        Info = info;
        Name = info.Name;
        FullName = info.FullName;
        DateCreated = info.CreationTimeUtc.ToString();
        DateModified = info.LastWriteTimeUtc.ToString();
        DateOpened = info.LastAccessTimeUtc.ToString();
        Extension = strings.Extension_Folder;
    }
}