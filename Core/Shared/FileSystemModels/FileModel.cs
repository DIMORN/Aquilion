namespace Shared;

public sealed class FileModel : FileSystemModel
{
    public string? Size { get; set; }
    public FileModel(FileSystemInfo info)
    {
        Info = info;
        Name = info.Name;
        FullName = info.FullName;
        DateCreated = info.CreationTimeUtc.ToString();
        DateModified = info.LastWriteTimeUtc.ToString();
        DateOpened = info.LastAccessTimeUtc.ToString();

        if (info is FileInfo fi) Size = Convert(fi.Length);
        FileSystemModelType = Shared.FileSystemModelType.File;

        Extension = (info.Extension.ToUpper()) switch
        {
            ".EXE" => strings.Extension_EXE,

            ".MSI" => strings.Extension_MSI,

            ".MP3" => strings.Extension_MP3_AAC_WMA,
            ".AAC" => strings.Extension_MP3_AAC_WMA,
            ".WMA" => strings.Extension_MP3_AAC_WMA,

            ".MIDI" => strings.Extension_MIDI,

            ".WAV" => strings.Extension_WAV,

            ".JPG" => strings.Extension_JPG,
            ".BMP" => strings.Extension_BMP,
            ".GIF" => strings.Extension_GIF,
            ".PNG" => strings.Extension_PNG,

            ".TXT" => strings.Extension_TXT,

            ".RTF" => strings.Extension_RTF,
            
            ".INF" => strings.Extension_INF,

            ".INI" => strings.Extension_INI,

            ".SYS" => strings.Extension_SYS,

            ".TTF" => strings.Extension_OTF_TTF,
            ".OTF" => strings.Extension_OTF_TTF,

            ".DLL" => strings.Extension_DLL,

            _ => $"File {info.Extension.ToUpper()}"
        };
    }
}