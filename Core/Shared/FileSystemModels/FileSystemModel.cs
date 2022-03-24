namespace Shared;

public abstract class FileSystemModel : BaseViewModel, IFileSystemModel
{
    public FileSystemInfo Info { get; set; }
    public string? Name { get; set; }
    public string? FullName { get; set; }
    public string? DateModified { get; set; }
    public string? DateCreated { get; set; }
    public string? DateOpened { get; set; }
    public FileSystemModelType? FileSystemModelType { get; set; }
    public string? Extension { get; set; }
    public FileSystemModel(FileSystemInfo info = null)
    {
        Info = info;
        Name = info.Name;
        FullName = info.FullName;
        DateCreated = info.CreationTimeUtc.ToString();
        DateModified = info.LastWriteTimeUtc.ToString();
        DateOpened = info.LastAccessTimeUtc.ToString();
    }
}