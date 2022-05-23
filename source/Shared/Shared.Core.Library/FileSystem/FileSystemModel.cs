namespace Shared.Core.Library.FileSystem
{
    public class FileSystemModel : BaseViewModel, IFileSystemModel, ISelectable
{
    public string? Name { get; set; }
    public string? FullName { get; set; }
    public string? Extension { get; set; }
    public string? Date { get; set; }
    public string? CreatedDate { get; set; }
    public bool? IsSelected { get; set; }

    public static FileSystemModel? Create(object obj, PropertyChangedEventHandler? selected = null)
    {

        return obj switch
        {
            FileInfo => new FileModel((FileInfo)obj, selected),
            DirectoryInfo => new DirectoryModel((DirectoryInfo)obj, selected),
            DriveInfo => new DriveModel((DriveInfo)obj, selected),
            StorageObject => new StorageObjectModel((StorageObject)obj, selected),
            _ => null
        };
    }

    private static readonly string[] Sizes;

    static FileSystemModel()
    {
        Sizes = new[] 
        { 
            Locale.Locale.FileSystem_Size_B, 
            Locale.Locale.FileSystem_Size_KB, 
            Locale.Locale.FileSystem_Size_MB, 
            Locale.Locale.FileSystem_Size_GB,
            Locale.Locale.FileSystem_Size_TB };
        
    }

    public static string LengthToString(long? value)
    {
        double len = (long)value;

        var order = 0;

        while (len >= 1024 && order < Sizes.Length - 1)
        {
            order++;
            len /= 1024;
        }

        return $"{len:0.##} {Sizes[order]}";
    }
}
    public sealed class StorageObjectModel : FileSystemModel
{
    public StorageObjectModel(StorageObject obj, PropertyChangedEventHandler? selected = null)
    {
        Name = obj.Name;
        FullName = obj.FullName;
        Extension = "Storage";
        PropertyChanged += selected;
        IsSelected = false;
    }
}

}
