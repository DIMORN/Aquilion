namespace Services.Images;

public class ImageWork
{
    public static string GetIconFileName(FileSystemModel s, int size = 48)
    {
        
        string filename = "\\";

        if(size == 48)
        {
            if (s.Extension == Locale.FileSystem_Extension_Directory)
                filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\48.ico";
            else
            if (s.Extension == Locale.FileSystem_Extension_Drive_HDD)
                filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\90.ico";
            else
            if (s.Extension == Locale.FileSystem_Extension_Drive_CD_DVD)
                filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\117.ico";
            else
            if (s.Extension == Locale.FileSystem_Extension_Drive_REMOVABLE)
                filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\81.ico";
            else
            if (s.Extension == Locale.FileSystem_Extension_Storage_Default)
            {
                if(s.Name == Locale.Storage_PhysicalDirectories_Names_MyDocuments)
                {
                    filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\849.ico";
                }
                else
                if(s.Name == Locale.Storage_PhysicalDirectories_Names_MyMusic)
                {
                    filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\873.ico";
                }
                else
                if (s.Name == Locale.Storage_Object_Names_PicturesVideoLibrary)
                {
                    filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\861.ico";
                }
                else
                if (s.Name == Locale.Storage_Object_Names_Downloads)
                {
                    filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\48.ico";
                }
            }
        }

        return filename;
    }
}