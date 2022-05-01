namespace Shared.Core.Library.FileSystem;

public sealed class FileModel : FileSystemModel
{
    public string? Size { get; set; }
    public long? LongSize { get; set; }

    public FileModel(FileInfo info, PropertyChangedEventHandler? selected = null)
    {
        Name = info.Name.Trim(info.Extension.ToCharArray());
        FullName = info.FullName;
        LongSize = info.Length;

        Date = info.LastWriteTimeUtc.ToString();

        Size = LengthToString(LongSize);

        Extension = info.Extension.ToLower() switch
        {
            ".exe" => Locale.Locale.FileSystem_Extension_EXE,
            ".txt" => Locale.Locale.FileSystem_Extension_TXT,
            ".rtf" => Locale.Locale.FileSystem_Extension_RTF,
            ".jpg" => Locale.Locale.FileSystem_Extension_JPG,
            ".bmp" => Locale.Locale.FileSystem_Extension_BMP,
            ".gif" => Locale.Locale.FileSystem_Extension_GIF,
            ".png" => Locale.Locale.FileSystem_Extension_PNG,
            ".mp3" => Locale.Locale.FileSystem_Extension_MP3_AAC,
            ".aac" => Locale.Locale.FileSystem_Extension_MP3_AAC,
            ".wav" => Locale.Locale.FileSystem_Extension_WAV,
            ".wma" => Locale.Locale.FileSystem_Extension_WMA,
            ".mid" => Locale.Locale.FileSystem_Extension_MIDI,
            ".sys" => Locale.Locale.FileSystem_Extension_SYS,
            ".inf" => Locale.Locale.FileSystem_Extension_INF,
            ".ini" => Locale.Locale.FileSystem_Extension_INI,
            ".dll" => Locale.Locale.FileSystem_Extension_DLL,
            ".htm" => Locale.Locale.FileSystem_Extension_HTML,
            ".html" => Locale.Locale.FileSystem_Extension_HTML,
            ".xml" => Locale.Locale.FileSystem_Extension_XML,
            ".xaml" => Locale.Locale.FileSystem_Extension_XAML,
            
            _ => $"{Locale.Locale.FileSystem_Extension_UnknownFile} {info.Extension.ToUpper()}"
        };

        IsSelected = false;

        PropertyChanged += selected;
    }
}

