namespace Shared;

public interface IFileSystemModel
{
    public string? Name { get; set; }
    public string? FullName { get; set; }
    public string? DateModified { get; set; }
    public string? DateCreated { get; set; }
    public string? DateOpened { get; set; }
    public FileSystemModelType? FileSystemModelType { get; set; }
    public string? Extension { get; set; }
}