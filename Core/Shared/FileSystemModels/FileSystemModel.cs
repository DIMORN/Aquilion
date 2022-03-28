namespace Shared;

public abstract class FileSystemModel : BaseViewModel, IFileSystemModel, ISelectableModel
{
    private static readonly string[] Sizes;
    public FileSystemInfo Info { get; set; }
    public string? Name { get; set; }
    public string? FullName { get; set; }
    public string? DateModified { get; set; }
    public string? DateCreated { get; set; }
    public string? DateOpened { get; set; }
    public string? Group { get; set; }
    public FileSystemModelType? FileSystemModelType { get; set; }
    public string? Extension { get; set; }
    public bool? IsSelected { get; set; }

    static FileSystemModel()
    {
        Sizes = new[] { strings.ModelLength_Size_Byte, strings.ModelLength_Size_KByte, strings.ModelLength_Size_MByte, strings.ModelLength_Size_GByte, strings.ModelLength_Size_TByte };
    }

    public string Convert(long l)
    {
        double len = l;

        var order = 0;

        while (len >= 1024 && order < Sizes.Length - 1)
        {
            order++;
            len /= 1024;
        }

        return $"{len:0.##} {Sizes[order]}";
    }
}

public interface ISelectableModel
{
    public bool? IsSelected { get; set; }
}