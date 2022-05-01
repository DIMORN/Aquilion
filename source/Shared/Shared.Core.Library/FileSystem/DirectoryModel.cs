namespace Shared.Core.Library.FileSystem;

public sealed class DirectoryModel : FileSystemModel
{
    public DirectoryModel(DirectoryInfo info, PropertyChangedEventHandler? selected = null)
    {
        Name = info.Name;
        FullName = info.FullName;

        Date = info.LastWriteTimeUtc.ToString();

        Extension = Locale.Locale.FileSystem_Extension_Directory;

        IsSelected = false;

        PropertyChanged += selected;
    }
}

