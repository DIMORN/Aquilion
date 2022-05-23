

namespace Services.Images
{
    public class ImageWork
    {
        private static ImageWork _instance;

        public static ImageWork Instance => _instance ??= new ImageWork();

        public virtual string GetIconFileName(FileSystemModel s, int size = 48)
        {

            string filename = "\\";

            if (size == 48)
            {
                if (s.Extension == Locale.FileSystem_Extension_Directory)
                    filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\31.ico";
                else
                if (s.Extension == Locale.FileSystem_Extension_SYS)
                    filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\531.ico";
                else
                if (s.Extension == Locale.FileSystem_Extension_INF)
                    filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\514.ico";
                else
                if (s.Extension == Locale.FileSystem_Extension_DLL)
                    filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\531.ico";
                else
                if (s.Extension == Locale.FileSystem_Extension_TXT)
                    filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\522.ico";
                else
                if (s.Extension == Locale.FileSystem_Extension_RTF)
                    filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\15.ico";
                else
                if (s.Extension == Locale.FileSystem_Extension_MP3_AAC)
                    filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\760.ico";
                else
                if (s.Extension == Locale.FileSystem_Extension_MIDI)
                    filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\772.ico";
                else
                if (s.Extension == Locale.FileSystem_Extension_WAV)
                    filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\772.ico";
                else
                if (s.Extension == Locale.FileSystem_Extension_HTML)
                    filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\661.ico";
                else
                if (s.Extension == Locale.FileSystem_Extension_MSI)
                    filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\1050.ico";
                else
                if (s.Extension == Locale.FileSystem_Extension_Drive_HDD)
                    filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\72.ico";
                else
                if (s.Extension == Locale.FileSystem_Extension_Drive_HDD)
                    filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\72.ico";
                else
                if (s.Extension == Locale.FileSystem_Extension_Drive_CD_DVD)
                    filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\99.ico";
                else
                if (s.Extension == Locale.FileSystem_Extension_Drive_REMOVABLE)
                    filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\63.ico";
                else
                if (s.Extension == Locale.FileSystem_Extension_Storage_Default)
                {
                    if (s.Name == Locale.Storage_PhysicalDirectories_Names_MyDocuments)
                    {
                        filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\804.ico";
                    }
                    else
                    if (s.Name == Locale.Storage_PhysicalDirectories_Names_MyMusic)
                    {
                        filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\828.ico";
                    }
                    else
                    if (s.Name == Locale.Storage_Object_Names_PicturesVideoLibrary)
                    {
                        filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\816.ico";
                    }
                    else
                    if (s.Name == Locale.Storage_Object_Names_Downloads)
                    {
                        filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\31.ico";
                    }
                    else
                    if (s.Name == Locale.Storage_Object_Names_Computer)
                    {
                        filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\131.ico";
                    }
                }
                else filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\11.ico";
            }
            else if (size == 16)
            {
                if (s.Extension == Locale.FileSystem_Extension_Directory)
                    filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\34.ico";
                else
                if (s.Extension == Locale.FileSystem_Extension_Storage_Default)
                {
                    if (s.Name == Locale.Storage_PhysicalDirectories_Names_MyDocuments)
                    {
                        filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\807.ico";
                    }
                    else
                    if (s.Name == Locale.Storage_PhysicalDirectories_Names_MyMusic)
                    {
                        filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\831.ico";
                    }
                    else
                    if (s.Name == Locale.Storage_Object_Names_PicturesVideoLibrary)
                    {
                        filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\819.ico";
                    }
                    else
                    if (s.Name == Locale.Storage_Object_Names_Downloads)
                    {
                        filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\34.ico";
                    }
                    else
                    if (s.Name == Locale.Storage_Object_Names_Computer)
                    {
                        filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\134.ico";
                    }
                }
                else if(s is DriveModel driveModel)
                {
                    if (driveModel.Extension == Locale.FileSystem_Extension_Drive_HDD)
                    {
                        filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\74.ico";
                    }
                    else
                        if (driveModel.Extension == Locale.FileSystem_Extension_Drive_CD_DVD)
                    {
                        filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\74.ico";
                    }
                    else
                        if (driveModel.Extension == Locale.FileSystem_Extension_Drive_REMOVABLE)
                    {
                        filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\65.ico";
                    }
                    else
                        if (driveModel.Extension == Locale.Storage_Object_Names_Downloads)
                    {
                        filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\34.ico";
                    }
                    else
                        if (driveModel.Extension == Locale.Storage_Object_Names_Computer)
                    {
                        filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\134.ico";
                    }
                }
                else filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\11.ico";
            }

            return filename;
        }

        public virtual string GetTaskIcon(MenuItemViewModel menuItemViewModel, int size = 16)
        {
            string filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\1098.ico";

            if (size == 16 & menuItemViewModel != null)
            {
                switch(menuItemViewModel.GroupName.ToLower())
                {
                    default:
                        if (menuItemViewModel.Header == "Make a new folder")
                            filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\1363.ico";
                        else if (menuItemViewModel.Header == "Publish this folder to the Web")
                            filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\883.ico";
                        else if (menuItemViewModel.Header == "Share this folder")
                            filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\1019.ico";
                        else filename = $"{Storage.Storage.ExternalServicesDirectory}\\Icons\\1098.ico";
                        break;
                }
            }
            menuItemViewModel.Icon = filename;
            return menuItemViewModel.Icon.ToString();
        }
    }
}